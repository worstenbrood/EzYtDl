using System;
using System.Linq;
using System.Collections.Generic;
using NAudio.Dsp;
using NAudio.Wave;
using TagLib;

namespace AudioTools
{
    public class AudioFile : AudioPlayer
    {
        // GetBpmX defaults
        public const float DefaultMinBpm = 95.0F;
        public const float DefaultMaxBpm = 180.0F;
        public const int DefaultPeakCount = 10;
        public const float DefaultLowPassCutoff = 175.0F;
        public const float DefaultHighPassCutoff = 90.0F;
        public const float DefaultTimeInSeconds = 0.5F; // Half a second
        
        private Tag _tag;
        
        public Tag Tag
        {
            get
            {
                if (_tag == null)
                {
                    using (var file = File.Create(AudioFile))
                    {
                        _tag = file.Tag;
                    }
                }

                return _tag;
            }
        }
        
        public AudioFile(string audioFile) : base(audioFile)
        {
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

            if (tempo < minBpm)
            {
                while (tempo < minBpm)
                {
                    tempo *= 2.0F;
                }
            }
            else if (tempo > maxBpm)
            {
                while (tempo > maxBpm)
                {
                    tempo /= 2.0F;
                }
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
            using (var reader = new MediaFoundationReader(AudioFile))
            {
                // First a lowpass to remove most of the song.
                var lowPass = BiQuadFilter.LowPassFilter(reader.WaveFormat.SampleRate, lowPassCutoff, 1.0F);

                // Now a highpass to remove the bassline.
                var highPass = BiQuadFilter.HighPassFilter(reader.WaveFormat.SampleRate, highPassCutoff, 1.0F);

                // Calculate bytes per sample
                var bytesPerSample = (uint)WaveFormat.BitsPerSample / 8;
                if (bytesPerSample == 0)
                {
                    bytesPerSample = 2;
                }

                var totalSamples = (ulong)(reader.Length / bytesPerSample);
                var timeInSamples = (uint)(WaveFormat.SampleRate * timeInSeconds); // Half a second
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
                    for (uint index = 0; index < samplesRead; index += (uint)WaveFormat.Channels)
                    {
                        var vol = 0.0F;

                        // Check volume on every channel
                        for (var channel = 0; channel < WaveFormat.Channels; channel++)
                        {
                            var sample = samples[index + channel];
                            var filtered = highPass.Transform(lowPass.Transform(sample));
                            if (vol < filtered)
                            {
                                vol = filtered;
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
        /// <returns>BPM groups ordered by count</returns>
        public int GetBpm(
            float minBpm = DefaultMinBpm,
            float maxBpm = DefaultMaxBpm,
            int peakCount = DefaultPeakCount,
            float lowPassCutoff = DefaultLowPassCutoff,
            float highPassCutoff = DefaultHighPassCutoff,
            float timeInSeconds = DefaultTimeInSeconds)
        {
            return GetBpmGroups(minBpm, maxBpm, peakCount, lowPassCutoff, highPassCutoff, timeInSeconds)
                .Select(g => g.Key)
                .FirstOrDefault();
        }
    }
}
