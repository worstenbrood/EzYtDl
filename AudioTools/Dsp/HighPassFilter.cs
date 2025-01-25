using NAudio.Dsp;

namespace AudioTools.Dsp
{
    public class HighPassFilter : IDsp
    {
        private volatile BiQuadFilter _filter;
        private readonly int _sampleRate;

        public HighPassFilter(int sampleRate, float cutoffFrequency, float q = 1.0F)
        {
            _sampleRate = sampleRate;
            _cutoffFrequency = cutoffFrequency;
            _q = q;
            _filter = BiQuadFilter.HighPassFilter(_sampleRate, _cutoffFrequency, _q);
        }

        private volatile float _cutoffFrequency;

        public float CutoffFrequency
        {
            set
            {
                _cutoffFrequency = value;
                _filter.SetHighPassFilter(_sampleRate, _cutoffFrequency, _q);

            }
        }

        private volatile float _q;

        public float Q
        {
            set
            {
                _q = value;
                _filter.SetHighPassFilter(_sampleRate, _cutoffFrequency, _q);
            }
        }

        public void SetParameters(float cutoffFrequency, float q = 1.0F)
        {
            _filter.SetHighPassFilter(_sampleRate, cutoffFrequency, q);
        }

        public float Transform(float sample)
        {
            return _filter.Transform(sample);
        }
    }
}
