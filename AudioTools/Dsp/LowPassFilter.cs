using NAudio.Dsp;

namespace AudioTools.Dsp
{
    public class LowPassFilter : IDsp
    {
        private volatile BiQuadFilter _filter;
        private readonly int _sampleRate;

        public LowPassFilter(int sampleRate, float cutoffFrequency, float q = 1.0F)
        {
            _sampleRate = sampleRate;
            _filter = BiQuadFilter.LowPassFilter(_sampleRate, cutoffFrequency, q);
        }

        public void SetParameters(float cutoffFrequency, float q = 1.0F)
        {
            _filter.SetLowPassFilter(_sampleRate, cutoffFrequency, q);
        }

        public float Transform(float sample)
        {
            return _filter.Transform(sample);
        }
    }
}
