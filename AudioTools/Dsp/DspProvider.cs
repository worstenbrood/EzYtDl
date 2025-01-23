using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;

namespace AudioTools.Tools
{
    public interface IDsp
    {
        float Transform(float sample);
    }

    public class DspProvider : ISampleProvider
    {
        private readonly ISampleProvider _baseProvider;
        private readonly List<IDsp> _dsps = new List<IDsp>();
        private readonly object _lock = new object();

        public DspProvider(ISampleProvider baseProvider)
        {
            _baseProvider = baseProvider;
        }

        public void Add(IDsp dsp)
        {
            lock (_lock)
            {
                _dsps.Add(dsp);
            }
        }

        public void Remove(IDsp dsp)
        {
            lock (_lock)
            {
                _dsps.Remove(dsp);
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var result = _baseProvider.Read(buffer, offset, count);
            for (int index = offset; index < offset + count; index++)
            {
                lock (_lock)
                {
                    buffer[index] = _dsps.Aggregate(buffer[index], (current, transform) => transform.Transform(current));
                }
            }

            return result;
        }

        public WaveFormat WaveFormat => _baseProvider.WaveFormat;
    }
}
