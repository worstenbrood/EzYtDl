using AudioTools.Dsp.Interfaces;

namespace AudioTools.Dsp
{
    public abstract class DspBase: ISampleDsp
    {
        private volatile bool _enabled;

        /// <summary>
        /// Enabled/disable dsp
        /// </summary>
        public bool Enabled
        {
            get =>  _enabled; 
            set => _enabled = value;
        }

        private volatile float _wet;

        /// <summary>
        /// Wet percentage (0 to 1)
        /// </summary>
        public float Wet
        {
            get => _wet;
            set => _wet = value;
        }

        private volatile float _dry;

        /// <summary>
        /// Dry percentage (0 to 1)
        /// </summary>
        public float Dry
        {
            get => _dry;
            set => _dry = value;
        }

        protected DspBase()
        {
            Enabled = true;
            Dry = 1.0F;
            Wet = 1.0F;
        }

        public abstract float TransformSample(float sample);

        public float Transform(float sample)
        {
            return sample * Dry + TransformSample(sample) * Wet;
        }
    }
}
