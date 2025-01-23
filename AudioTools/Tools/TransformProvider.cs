using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace AudioTools.Tools
{
    public class TransformProvider : ISampleProvider
    {
        private readonly ISampleProvider _baseProvider;

        public TransformProvider(ISampleProvider baseProvider)
        {
            _baseProvider = baseProvider;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var result = _baseProvider.Read(buffer, offset, count);
            return result;
        }

        public WaveFormat WaveFormat => _baseProvider.WaveFormat;
    }
}
