using NAudio.Wave;
using SoundTouch;
using System;
using AudioTools.Extensions;

namespace AudioTools
{
    public class ReadEventArgs : EventArgs
    {
        public readonly byte[] Buffer;
        public readonly int Offset;
        public readonly int BytesRead;

        public ReadEventArgs(byte[] buffer, int offset, int bytesRead)
        {
            Buffer = buffer;
            Offset = offset;
            BytesRead = bytesRead;
        }
    }
    
    public delegate void ReadEventHandler(object sender, ReadEventArgs args);

    /// <summary>
    /// NAudio WaveStream class for processing audio stream with SoundTouch effects
    /// </summary>
    public class SoundTouchWaveProvider : IWaveProvider, IDisposable
    {
        private const int BufferSize = 2048;
        private readonly IWaveProvider _input;
        private readonly SoundTouchProcessor _processor;
        private readonly byte[] _bytebuffer = new byte[BufferSize];
        private readonly int _floatsPerSample;
        private MediaFoundationResampler _sampler;
        private float[] _floatBuffer = new float[BufferSize / sizeof(float)];
        private bool _endReached = false;
        private BpmDetect _bpmDetect;

        // Events
        public event ReadEventHandler OnRead;

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
                // Save to dispose
                _sampler = new MediaFoundationResampler(input, WaveFormat.CreateIeeeFloatWaveFormat(input.WaveFormat.SampleRate, input.WaveFormat.Channels));
                _input = _sampler;
            }
            else
            {
                _input = input;
            }

            // Save floats per sample
            _floatsPerSample = _input.WaveFormat.BitsPerSample / sizeof(float);

            // Init SoundTouch processor
            _processor = processor;
            _processor.Init(WaveFormat);
            
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
                        // End of stream. flush final samples from SoundTouch buffers to output
                        if (_endReached == false)
                        {
                            _endReached = true;  // Do only once to avoid continuous flushing
                            _processor.Flush();
                        }
                        break;
                    }

                    // Binary copy data from "byte[]" to "float[]" buffer
                    _bytebuffer.BlockCopy(0, _floatBuffer, 0, bytesRead);

                    // Process samples
                    _processor.PutSamples(_floatBuffer, (uint)(bytesRead / _floatsPerSample));
                }

                // Ensure that buffer is large enough to receive desired amount of data out
                _floatBuffer = _floatBuffer.EnsureBufferSize(count / sizeof(float));
                
                // Get processed output samples from SoundTouch
                int numSamples = (int)_processor.ReceiveSamples(_floatBuffer, (uint)(count / _floatsPerSample));

                // Feed bpm detect
                _bpmDetect?.PutSamples(_floatBuffer, (uint)(count / _floatsPerSample));

                // Calculate total bytes
                var totalBytes = numSamples * _floatsPerSample;

                // Binary copy data from "float[]" to "byte[]" buffer
                _floatBuffer.BlockCopy(0, buffer, offset, totalBytes);

                // Invoke on read
                OnRead?.Invoke(this, new ReadEventArgs(buffer, offset, totalBytes));

                // Number of bytes
                return totalBytes;  
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
