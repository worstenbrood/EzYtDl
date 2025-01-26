using System;
using NAudio.Wave;
using SoundTouch;

namespace AudioTools.SoundTouch
{
    public class BpmDetectProcessor : IDisposable
    {
        private readonly ISampleProvider _input;
        private readonly BpmDetect _bpmDetect;
        private readonly float[] _buffer = new float[8192];

        public BpmDetectProcessor(ISampleProvider input)
        {
            _input = input;
            _bpmDetect = new BpmDetect(_input.WaveFormat.Channels, _input.WaveFormat.SampleRate);
        }
        
        public float GetBpm()
        {
            int samplesRead;

            do
            {
                samplesRead = _input.Read(_buffer, 0, _buffer.Length);
                if (samplesRead > 0)
                {
                    _bpmDetect.PutSamples(_buffer, (uint)(samplesRead / WaveFormat.Channels));
                }
            } while (samplesRead > 0);
            
            return _bpmDetect.Bpm;
        }

        public WaveFormat WaveFormat => _input.WaveFormat;

        public virtual void Dispose()
        {
            _bpmDetect?.Dispose();
        }
    }
}
