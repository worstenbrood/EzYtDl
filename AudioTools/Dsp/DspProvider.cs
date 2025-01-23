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
        private ISampleProvider _baseProvider;
        private readonly List<IDsp> _dsp = new List<IDsp>();
        private readonly object _lock = new object();

        public DspProvider()
        {
        }

        public void SetBaseProvider(ISampleProvider baseProvider)
        {
            _baseProvider = baseProvider;
        }

        public DspProvider(ISampleProvider baseProvider)
        {
            _baseProvider = baseProvider;
        }

        public void Add(IDsp dsp)
        {
            lock (_lock)
            {
                _dsp.Add(dsp);
            }
        }

        public void Add(int index, IDsp dsp)
        {
            lock (_lock)
            {
                _dsp.Insert(index, dsp);
            }
        }

        public void Remove(IDsp dsp)
        {
            lock (_lock)
            {
                _dsp.Remove(dsp);
            }
        }

        public void Remove(int index)
        {
            lock (_lock)
            {
                _dsp.RemoveAt(index);
            }
        }

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _dsp.Count;
                }
            }
        }

        public IDsp GetDsp(int index)
        {
            lock (_lock)
            {
                return _dsp[index];
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var result = _baseProvider.Read(buffer, offset, count);
            for (int index = offset; index < offset + count; index++)
            {
                lock (_lock)
                {
                    buffer[index] = _dsp.Aggregate(buffer[index], (current, transform) => transform.Transform(current));
                }
            }

            return result;
        }

        public WaveFormat WaveFormat => _baseProvider.WaveFormat;
    }
}
