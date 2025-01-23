using NAudio.Dsp;

namespace AudioTools.Tools
{
    public class LowPassFilter : IDsp
    {
        private volatile BiQuadFilter _filter;
        private readonly int _sampleRate;

        public LowPassFilter(int sampleRate, float cutoffFrequency, float q)
        {
            _sampleRate = sampleRate;
            _filter = BiQuadFilter.LowPassFilter(_sampleRate, cutoffFrequency, q);
        }

        public void SetParameters(float cutoffFrequency, float q)
        {
            _filter.SetLowPassFilter(_sampleRate, cutoffFrequency, q);
        }

        public float Transform(float sample)
        {
            return _filter.Transform(sample);
        }
    }
}
