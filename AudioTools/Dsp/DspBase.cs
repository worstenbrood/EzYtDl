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

        private volatile float _dry;
        private volatile float _wet;
        
        /// <summary>
        /// Dry/Wet percentage (0.0 to 1.0)
        /// </summary>
        public float DryWet
        {
            set
            {
                if (value >= 0f && value <= 1f)
                {
                    if (value < 0.5f)
                    {
                        _dry = 1f - value;
                        _wet = 1f - _dry;
                    }
                    else if (value >= 0.5f)
                    {
                        _wet = value;
                        _dry = 1f - _wet;
                    }
                }
            }
        }

        protected DspBase()
        {
            Enabled = true;
            DryWet = 0.5f;
        }

        public abstract float TransformSample(float sample);

        public float Transform(float sample)
        {
            return sample * _dry + TransformSample(sample) * _wet;
        }
    }
}
