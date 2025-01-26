using AudioTools.Dsp.Interfaces;
using System;

namespace AudioTools.Dsp
{
    /// <summary>
    /// Bitcrusher based on https://github.com/bdejong/musicdsp/blob/master/source/Effects/139-lo-fi-crusher.rst
    /// </summary>
    public class BitCrusher : ISampleDsp
    {
        private readonly int _sampleRate;
        private volatile float _normFreq;
        private volatile float _step;
        
        public float Frequency
        {
            set => _normFreq = value / _sampleRate;
        }
        
        public float Bits
        {
            set => _step = 1.0f / (float)Math.Pow(2, value);
        }

        public BitCrusher(int sampleRate, float frequency, float bits)
        {
            // Save sample rate
            _sampleRate = sampleRate;
            
            // Set parameters
            Frequency = frequency;
            Bits = bits;
        }
        
        private volatile float _phaser;
        private volatile float _last;

        public float Transform(float sample)
        {
            _phaser = _phaser + _normFreq;
            if (_phaser < 1.0f)
            {
                return _last;
            }
            _phaser = _phaser - 1.0f;
            return _last = _step * (float)Math.Floor(sample / _step + 0.5f);
        }

        public void Reset()
        {
            _phaser = 0.0f;
            _last = 0.0f;
        }
    }
}
