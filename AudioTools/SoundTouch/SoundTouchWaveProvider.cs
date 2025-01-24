using NAudio.Wave;
using SoundTouch;
using System;

namespace AudioTools
{
    /// <summary>
    /// NAudio WaveStream class for processing audio stream with SoundTouch effects
    /// </summary>
    public class SoundTouchWaveProvider : IWaveProvider, IDisposable
    {
        private readonly IWaveProvider _input;
        private readonly SoundTouchProcessor _processor;

        private const int BufferSize = 4096;
        private readonly byte[] _bytebuffer = new byte[BufferSize];
        private float[] _floatBuffer = new float[BufferSize / sizeof(float)];
        private bool _endReached = false;
        private readonly int _floatsPerSample;
        private readonly BpmDetect _bpmDetect;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">WaveProvider</param>
        /// <param name="processor">SoundTouchProcessor (optional)</param>
        /// <param name="detectBpm"></param>
        public SoundTouchWaveProvider(IWaveProvider input, SoundTouchProcessor processor, bool detectBpm = false)
        {
            // Resample if necessary
            if (input.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat || input.WaveFormat.BitsPerSample != 32)
            {
                _input = new MediaFoundationResampler(input, WaveFormat.CreateIeeeFloatWaveFormat(input.WaveFormat.SampleRate, input.WaveFormat.Channels));
            }
            else
            {
                _input = input;
            }

            _floatsPerSample = _input.WaveFormat.BitsPerSample / sizeof(float);

            // Init SoundTouch processor
            _processor = processor;
            _processor.Channels = (uint)_input.WaveFormat.Channels;
            _processor.SampleRate = (uint)_input.WaveFormat.SampleRate;

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
        public int Read(byte[] buffer, int offset, int count)
        {
            try
            {
                // Iterate until enough samples available for output:
                // - read samples from input stream
                // - put samples to SoundStretch processor
                while (_processor.AvailableSampleCount < count)
                {
                    int bytesRead = _input.Read(_bytebuffer, 0, _bytebuffer.Length);
                    if (bytesRead == 0)
                    {
                        // end of stream. flush final samples from SoundTouch buffers to output
                        if (_endReached == false)
                        {
                            _endReached = true;  // do only once to avoid continuous flushing
                            _processor.Flush();
                        }
                        break;
                    }

                    // binary copy data from "byte[]" to "float[]" buffer
                    Buffer.BlockCopy(_bytebuffer, 0, _floatBuffer, 0, bytesRead);
                    _processor.PutSamples(_floatBuffer, (uint)(bytesRead / _floatsPerSample));
                }

                // ensure that buffer is large enough to receive desired amount of data out
                if (_floatBuffer.Length < count / sizeof(float))
                {
                    _floatBuffer = new float[count / sizeof(float)];
                }
                
                // get processed output samples from SoundTouch
                int numSamples = (int)_processor.ReceiveSamples(_floatBuffer, (uint)(count / _floatsPerSample));

                // Feed bpm detect
                _bpmDetect?.PutSamples(_floatBuffer, (uint)(count / _floatsPerSample));

                // binary copy data from "float[]" to "byte[]" buffer
                Buffer.BlockCopy(_floatBuffer, 0, buffer, offset, numSamples * _floatsPerSample);
                return numSamples * _floatsPerSample;  // number of bytes
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
        }
    }
}
