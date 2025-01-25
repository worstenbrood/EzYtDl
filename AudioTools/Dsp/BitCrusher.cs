using System;

namespace AudioTools.Dsp
{
    /// <summary>
    /// Bitcrusher based on https://github.com/bdejong/musicdsp/blob/master/source/Effects/139-lo-fi-crusher.rst
    /// </summary>
    public class BitCrusher : IDsp
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
            set => _step = 1f / (float)Math.Pow(value, 2f);
        }

        public BitCrusher(int sampleRate, float frequency, float bits)
        {
            // Save sample rate
            _sampleRate = sampleRate;
            
            // Set parameters
            Frequency = frequency;
            Bits = bits;
        }
        
        private float _phaser;
        private float _last;

        public float Transform(float sample)
        {
            _phaser += _normFreq;
            if (_phaser < 1.0f)
            {
                return _last;
            }
            _phaser -= 1.0f;
            return _last = _step * (float)Math.Floor(sample / _step + 0.5f);
        }
    }
}
