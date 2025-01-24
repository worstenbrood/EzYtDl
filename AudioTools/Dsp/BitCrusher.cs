namespace AudioTools.Dsp
{
    public class BitCrusher : IDsp
    {
        private readonly int _sampleRate;
        private float _normFreq;
        private int _bits;
        private float _phaser;
        private float _last;

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
                _last = step * sample / step + 0.5f;
                return _last;
            }
        }
    }
}
