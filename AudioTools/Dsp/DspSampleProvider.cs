using System.Linq;
using AudioTools.Dsp.Interfaces;
using AudioTools.Utils;
using NAudio.Wave;

namespace AudioTools.Dsp
{
    public class DspSampleProvider : LockedList<ISampleDsp>, ISampleProvider
    {
        private ISampleProvider _input;

        public DspSampleProvider(ISampleProvider input)
        {
            _input = input;
        }
        
        public DspSampleProvider()
        {
        }

        public void SetInput(ISampleProvider input)
        {
            _input = input;
        }
        
        public int Read(float[] buffer, int offset, int count)
        {
            var result = _input.Read(buffer, offset, count);
            if (Count > 0)
            {
                lock (ListLock)
                {
                    for (int index = offset; index < offset + count; index++)
                    {
                        // Apply all DSP 
                        buffer[index] = List
                            .Where(t => t.Enabled)
                            .Aggregate(buffer[index], (current, transform) => transform.Transform(current));
                    }
                }
            }

            return result;
        }

        public WaveFormat WaveFormat => _input.WaveFormat;
    }
}
