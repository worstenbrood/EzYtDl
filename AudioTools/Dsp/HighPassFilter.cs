using NAudio.Dsp;

namespace AudioTools.Dsp
{
    public class HighPassFilter : IDsp
    {
        private volatile BiQuadFilter _filter;
        private readonly int _sampleRate;

        public HighPassFilter(int sampleRate, float cutoffFrequency, float q)
        {
            _sampleRate = sampleRate;
            _filter = BiQuadFilter.HighPassFilter(_sampleRate, cutoffFrequency, q);
        }

        public void SetParameters(float cutoffFrequency, float q)
        {
            _filter.SetHighPassFilter(_sampleRate, cutoffFrequency, q);
        }

        public float Transform(float sample)
        {
            return _filter.Transform(sample);
        }
    }
}
