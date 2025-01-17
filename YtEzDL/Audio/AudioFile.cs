using NAudio.Dsp;
using NAudio.Wave;
using System;
using System.Linq;
using System.Collections.Generic;

namespace YtEzDL.Audio
{
    public class AudioFile
    {
        // GetBpmX defaults
        public const float DefaultMinBpm = 90.0F;
        public const float DefaultMaxBpm = 180.0F;
        public const int DefaultPeakCount = 10;
        public const float DefaultLowPassCutoff = 150.0F;
        public const float DefaultHighPassCutoff = 100.0F;
        public const float DefaultTimeInSeconds = 0.5F; // Half a second
        
        private readonly string _audioFile;

        private WaveFormat _waveFormat;
        public WaveFormat WaveFormat {
            get
            {
                if (_waveFormat == null)
                {
                    using (var reader = new MediaFoundationReader(_audioFile))
                    {
                        _waveFormat = reader.WaveFormat;
                    }
                }

                return _waveFormat;
            }

            private set => _waveFormat = value;
        }

        public AudioFile(string audioFile)
        {
            _audioFile = audioFile;
        }

        private struct Peak
        {
            public ulong Position;
            public float Volume;

            public void Reset()
            {
                Position = 0;
                Volume = 0.0F;
            }
        }

        private short GetTempo(Peak[] peaks, int peak, int index, float minBpm, float maxBpm)
        {
            var tempo = 60.0F * WaveFormat.SampleRate / (peaks[peak + index].Position - peaks[peak].Position);
            while (tempo < minBpm)
            {
                tempo *= 2.0F;
            }
            while (tempo > maxBpm)
            {
                tempo /= 2.0F;
            }

            return (short)Math.Round(tempo);
        }

        /// <summary>
        /// Get BPM for the current AudioFile.
        /// </summary>
        /// <param name="minBpm">Minimum BPM (Used for correcting)</param>
        /// <param name="maxBpm">Maximum BPM (Used for correcting)</param>
        /// <param name="peakCount">Number of Peak objects </param>
        /// <param name="lowPassCutoff">Low pass filter cutoff frequency</param>
        /// <param name="highPassCutoff">High pass filter cutoff frequency</param>
        /// <param name="timeInSeconds">Time in seconds for every part</param>
        /// <returns>BPM groups ordered by count</returns>
        public IOrderedEnumerable<IGrouping<short, short>> GetBpmGroups(
            float minBpm = DefaultMinBpm, 
            float maxBpm = DefaultMaxBpm, 
            int peakCount = DefaultPeakCount, 
            float lowPassCutoff = DefaultLowPassCutoff,
            float highPassCutoff = DefaultHighPassCutoff,
            float timeInSeconds = DefaultTimeInSeconds)
        {
            // Load the file
            using (var reader = new MediaFoundationReader(_audioFile))
            {
                // Store waveFormat
                _waveFormat = reader.WaveFormat;

                // First a lowpass to remove most of the song.
                var lowPass = BiQuadFilter.LowPassFilter(_waveFormat.SampleRate, lowPassCutoff, 1.0F);

                // Now a highpass to remove the bassline.
                var highPass = BiQuadFilter.HighPassFilter(_waveFormat.SampleRate, highPassCutoff, 1.0F);

                // Calculate bytes per sample
                var bytesPerSample = (uint)_waveFormat.BitsPerSample / 8;
                if (bytesPerSample == 0)
                {
                    bytesPerSample = 2;
                }

                var totalSamples = (ulong)(reader.Length / bytesPerSample);
                var timeInSamples = (uint)(_waveFormat.SampleRate * timeInSeconds); // Half a second
                var peaks = new Peak[totalSamples / timeInSamples + 1];
                var samples = new float[timeInSamples];
                var sampleProvider = reader.ToSampleProvider();

                // What we're going to do here, is to divide up our audio into parts.
                // We will then identify, for each part, what the loudest sample is in that
                // part.
                // It's implied that that sample would represent the most likely 'beat'
                // within that part.
                // Each part is 0.5 seconds long
                // This will give us 60 'beats' - we will only take the loudest half of
                // those.
                // This will allow us to ignore breaks, and allow us to address tracks with
                // a BPM below 120.

                var maxPeak = new Peak();

                // Read samples of every 0.5 second
                for (ulong offset = 0; offset < totalSamples; offset += timeInSamples)
                {
                    // Reset to defaults
                    maxPeak.Reset();

                    var samplesRead = sampleProvider.Read(samples, 0, (int)timeInSamples);

                    // Enumerate samples of 0.5 second
                    for (uint index = 0; index < samplesRead; index += (uint)_waveFormat.Channels)
                    {
                        var vol = 0.0F;

                        // Check volume on every channel
                        for (var channel = 0; channel < _waveFormat.Channels; channel++)
                        {
                            var value = highPass.Transform(lowPass.Transform(samples[index + channel]));
                            if (vol < value)
                            {
                                vol = value;
                            }
                        }

                        if (maxPeak.Position != 0 && maxPeak.Volume >= vol)
                        {
                            continue;
                        }

                        maxPeak.Position = offset * bytesPerSample + index * bytesPerSample;
                        maxPeak.Volume = vol;
                    }

                    peaks[offset / timeInSamples] = maxPeak;
                }

                peaks = peaks
                    // We then sort the peaks according to volume...
                    .OrderByDescending(p => p.Volume)
                    // ...take the loudest half of those...
                    .Take(peaks.Length / 2)
                    // Sort by position
                    .OrderBy(p => p.Position)
                    .ToArray();

                var bpm = new List<short>();
                for (var peak = 0; peak < peaks.Length; peak++)
                {
                    for (var index = 1; peak + index < peaks.Length && index < peakCount; index++)
                    {
                        bpm.Add(GetTempo(peaks, peak, index, minBpm, maxBpm));
                    }
                }

                return bpm
                    // Group by bpm
                    .GroupBy(t => t)
                    // Order by count
                    .OrderByDescending(g => g.Count());
            }
        }

        /// <summary>
        /// Get BPM for the current AudioFile.
        /// </summary>
        /// <param name="minBpm">Minimum BPM (Used for correcting)</param>
        /// <param name="maxBpm">Maximum BPM (Used for correcting)</param>
        /// <param name="peakCount">Number of Peak objects </param>
        /// <param name="lowPassCutoff">Low pass filter cutoff frequency</param>
        /// <param name="highPassCutoff">High pass filter cutoff frequency</param>
        /// <param name="timeInSeconds">Time in seconds for every part</param>
        /// <returns>BPM</returns>
        public short GetBpm(
            float minBpm = DefaultMinBpm,
            float maxBpm = DefaultMaxBpm,
            int peakCount = DefaultPeakCount,
            float lowPassCutoff = DefaultLowPassCutoff,
            float highPassCutoff = DefaultHighPassCutoff,
            float timeInSeconds = DefaultTimeInSeconds)
        {
            var groups = GetBpmGroups(minBpm, maxBpm, peakCount, lowPassCutoff, highPassCutoff, timeInSeconds);
            return groups
                .Select(s => s.Key)
                // Return first group
                .FirstOrDefault();
        }
    }
}
