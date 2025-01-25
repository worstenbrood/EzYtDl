using System.Linq;
using AudioTools.Dsp.Interfaces;
using AudioTools.Utils;
using NAudio.Wave;

namespace AudioTools.Dsp
{
    public class DspSampleProvider : LockedList<ISampleDsp>, ISampleProvider
    {
        private ISampleProvider _baseProvider;
       
        public DspSampleProvider()
        {
        }

        public void SetBaseProvider(ISampleProvider baseProvider)
        {
            _baseProvider = baseProvider;
        }

        public DspSampleProvider(ISampleProvider baseProvider)
        {
            _baseProvider = baseProvider;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var result = _baseProvider.Read(buffer, offset, count);
            if (Count > 0)
            {
                lock (ListLock)
                {
                    for (int index = offset; index < offset + count; index++)
                    {
                        // Apply all DSP 
                        buffer[index] = List.Aggregate(buffer[index], (current, transform) => transform.Transform(current));
                    }
                }
            }

            return result;
        }

        public WaveFormat WaveFormat => _baseProvider.WaveFormat;
    }
}
