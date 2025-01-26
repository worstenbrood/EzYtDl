using System;
using NAudio.Wave;
using SoundTouch;

namespace AudioTools
{
    /// <summary>
    /// Provides a SoundTouch 
    /// </summary>
    public class AudioSampleProvider : ISampleProvider, IDisposable
    {
        private readonly SoundTouchProcessor _processor;
        private readonly MediaFoundationReader _reader;
        private readonly SoundTouchSampleProvider _soundTouchStream;

        public static readonly MediaFoundationReader.MediaFoundationReaderSettings Settings = new
            MediaFoundationReader.MediaFoundationReaderSettings
            {
                RequestFloatOutput = true,
                RepositionInRead = true,
            };

        public AudioSampleProvider(string audioFile, SoundTouchProcessor processor, bool calculateBpm = false)
        {
            _processor = processor;
            _reader = new MediaFoundationReader(audioFile, Settings);
            _soundTouchStream = new SoundTouchSampleProvider(_reader, _processor, calculateBpm);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            return _soundTouchStream.Read(buffer, offset, count);
        }

        public long Position
        {
            get => _reader.Position;
            set
            {
                var position = _reader.WaveFormat.AverageBytesPerSecond * value;
                position += position % _reader.BlockAlign;

                if (position > 0 && position < _reader.Length)
                {
                    // Clear data
                    _processor.Clear();

                    // Set position
                    _reader.Position = position;
                }
            }
        }
        
        public float Bpm => _soundTouchStream?.Bpm ?? 0f;
        
        public WaveFormat WaveFormat => _soundTouchStream.WaveFormat;

        public virtual void Dispose()
        {
            _reader?.Dispose();
            _soundTouchStream?.Dispose();
        }
    }
}
