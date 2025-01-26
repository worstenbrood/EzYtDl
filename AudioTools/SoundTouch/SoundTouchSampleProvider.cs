using NAudio.Wave;
using SoundTouch;
using System;

namespace AudioTools
{
    /// <summary>
    /// NAudio SampleProvider class for processing audio stream with SoundTouch effects
    /// </summary>
    public class SoundTouchSampleProvider : ISampleProvider, IDisposable
    {
        private const int BufferSize = 8192;

        private readonly float[] _buffer = new float[BufferSize];
        private readonly ISampleProvider _input;
        private readonly SoundTouchProcessor _processor;
        
        private MediaFoundationResampler _sampler;
        private BpmDetect _bpmDetect;
        private bool _endReached;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">WaveProvider</param>
        /// <param name="processor">SoundTouchProcessor (optional)</param>
        /// <param name="detectBpm"></param>
        public SoundTouchSampleProvider(IWaveProvider input, SoundTouchProcessor processor, bool detectBpm = false)
        {
            // Resample if necessary
            if (input.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat || input.WaveFormat.BitsPerSample != 32)
            {
                // Save to dispose
                _sampler = new MediaFoundationResampler(input, WaveFormat.CreateIeeeFloatWaveFormat(input.WaveFormat.SampleRate, input.WaveFormat.Channels));

                // Store sample provider
                _input = _sampler.ToSampleProvider();
            }
            else
            {
                // Store sample provider
                _input = input.ToSampleProvider();
            }

            // Init SoundTouch processor
            _processor = processor;
            _processor.Init(_input.WaveFormat);
            
            // Enable bpm detection
            if (detectBpm)
            {
                _bpmDetect = new BpmDetect(_input.WaveFormat.Channels, _input.WaveFormat.SampleRate);
            }
        }
        
        public WaveFormat WaveFormat => _input.WaveFormat;

        public float Tempo
        {
           set => _processor.Tempo = value;
        }

        public float TempoChange
        {
            set => _processor.TempoChange = value;
        }

        public float Rate
        {
            set => _processor.Rate = value;
        }

        public float RateChange
        {
            set => _processor.RateChange = value;
        }

        public float Pitch
        {
            set => _processor.Pitch = value;
        }

        public float PitchOctaves
        {
            set => _processor.PitchOctaves = value;
        }

        public float PitchSemiTones
        {
            set => _processor.PitchSemiTones = value;
        }
        
        public float Bpm => _bpmDetect?.Bpm ?? 0.0F;

        /// <summary>
        /// Overridden Read function that returns samples processed with SoundTouch. Returns data in same format as
        /// WaveChannel32 i.e. stereo float samples.
        /// </summary>
        /// <param name="buffer">Buffer where to return sample data</param>
        /// <param name="offset">Offset from beginning of the buffer</param>
        /// <param name="count">Number of bytes to return</param>
        /// <returns>Number of bytes copied to buffer</returns>
        public int Read(float[] buffer, int offset, int count)
        {
            try
            {
                // Iterate until enough samples available for output:
                // - read samples from input stream
                // - put samples to SoundStretch processor
                while (_processor.AvailableSampleCount < count)
                {
                    int samplesRead = _input.Read(_buffer, 0, _buffer.Length);
                    if (samplesRead == 0)
                    {
                        // End of stream. flush final samples from SoundTouch buffers to output
                        if (_endReached == false)
                        {
                            _endReached = true;  // Do only once to avoid continuous flushing
                            _processor.Flush();
                        }
                        break;
                    }

                    // Process samples
                    _processor.PutSamples(_buffer, (uint)(samplesRead / WaveFormat.Channels));
                }
                
                // Get processed output samples from SoundTouch
                uint numSamples = _processor.ReceiveSamples(buffer, (uint)(count / WaveFormat.Channels));

                // Feed bpm detect
                _bpmDetect?.PutSamples(buffer, numSamples / (uint)WaveFormat.Channels);
                
                // Number of bytes
                return (int)numSamples * WaveFormat.Channels;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Clear the internal processor buffers. Call this if seeking or rewinding to new position within the stream.
        /// </summary>
        public void Clear()
        {
            _processor.Clear();
            _endReached = false;
        }

        public void Dispose()
        {
            _bpmDetect?.Dispose();
            _bpmDetect = null;

            _sampler?.Dispose();
            _sampler = null;
        }
    }
}
