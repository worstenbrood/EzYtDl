namespace AudioTools.Dsp
{
    /// <summary>
    /// Bitcrusher based on https://github.com/bdejong/musicdsp/blob/master/source/Effects/139-lo-fi-crusher.rst
    /// </summary>
    public class BitCrusher : IDsp
    {
        private readonly int _sampleRate;
        private float _normFreq;
        private int _bits;
        
        public BitCrusher(int sampleRate, int frequency, int bits)
        {
            _sampleRate = sampleRate;
            _normFreq = (float) frequency / sampleRate;
            _bits = bits;
        }

        public void SetParameters(int frequency, int bits)
        {
            lock (this)
            {
                _normFreq = (float)frequency / _sampleRate;
                _bits = bits;
            }
        }

        private float _phaser;
        private float _last;

        public float Transform(float sample)
        {
            lock (this)
            {
                var step = 1 / 2 ^ _bits;
                _phaser += _normFreq;
                if (_phaser < 1.0f)
                {
                    return _last;
                }

                _phaser -= 1.0f;
                return _last = step * sample / step + 0.5f;
            }
        }
    }
}
