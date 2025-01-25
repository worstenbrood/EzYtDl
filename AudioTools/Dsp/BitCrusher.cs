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

        public BitCrusher(int sampleRate, int frequency, int bits)
        {
            _sampleRate = sampleRate;
            _normFreq = (float)frequency / sampleRate;
            _step = 1 / (float)Math.Pow(bits, 2);
        }

        public void SetParameters(int frequency, int bits)
        {
            _normFreq = (float)frequency / _sampleRate;
            _step = 1 / (float)Math.Pow(bits, 2);
        }

        private float _phaser;
        private float _last;

        public float Transform(float sample)
        {
            _phaser += _normFreq;
            if (_phaser >= 1.0f)
            {
                _phaser -= 1.0f;
                _last = _step * (float)Math.Floor(sample / _step + 0.5f);
            }
            
            return _last;
        }
    }
}
