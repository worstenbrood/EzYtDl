using AudioTools.Dsp.Interfaces;

namespace AudioTools.Dsp
{
    public abstract class DspBase: ISampleDsp
    {
        private volatile bool _enabled;

        public bool Enabled
        {
            get =>  _enabled; 
            set => _enabled = value;
        }

        protected DspBase()
        {
            Enabled = true;
        }

        public abstract float TransformSample(float sample);

        public float Transform(float sample)
        {
            return TransformSample(sample);
        }
    }
}
