using System;
using System.IO;
using NAudio.Wave;
using System.Threading;
using AudioTools.Tools;

namespace AudioTools
{
    public class AudioPlayer : IDisposable
    {
        public static void Play(string audioFile)
        {
            using (var reader = new MediaFoundationReader(audioFile))
            {
                var autoResetEvent = new AutoResetEvent(false);
                var wasapiOut = new WasapiOut();
                wasapiOut.PlaybackStopped += (o, e) => autoResetEvent.Set();
                wasapiOut.Init(reader.ToSampleProvider());
                wasapiOut.Play();
                autoResetEvent.WaitOne();
            }
        }

        protected readonly string AudioFile;
        private WasapiOut _wasapiOut;
        private MediaFoundationReader _reader;

        public DspProvider Dsp { get; private set; }

        private long? _lengthInBytes;

        public long LengthInBytes
        {
            get
            {
                if (_lengthInBytes == null)
                {
                    SetMediaProperties();
                }

                return _lengthInBytes.GetValueOrDefault();
            }
        }

        private WaveFormat _waveFormat;

        public WaveFormat WaveFormat
        {
            get
            {
                if (_waveFormat == null)
                {
                    SetMediaProperties();
                }

                return _waveFormat;
            }
        }

        private TimeSpan _time;

        public TimeSpan Time
        {
            get
            {
                if (_time == TimeSpan.Zero)
                {
                    SetMediaProperties();
                }

                return _time;
            }
        }

        private void SetMediaProperties()
        {
            using (var reader = new MediaFoundationReader(AudioFile))
            {
                _waveFormat = reader.WaveFormat;
                _lengthInBytes = reader.Length;
                _time = TimeSpan.FromSeconds((double)_lengthInBytes / _waveFormat.AverageBytesPerSecond);
            }
        }

        public AudioPlayer(string audioFile)
        {
            AudioFile = audioFile;
            Dsp = new DspProvider();
        }
        
        public void Play()
        {
            if (_wasapiOut != null)
            {
                switch (_wasapiOut.PlaybackState)
                {
                    case PlaybackState.Paused:
                        _wasapiOut.Play();
                        break;

                    case PlaybackState.Stopped:
                    case PlaybackState.Playing:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                _reader = new MediaFoundationReader(AudioFile);
                _wasapiOut = new WasapiOut();
                Dsp.SetBaseProvider(_reader.ToSampleProvider());
                _wasapiOut.Init(Dsp);
                _wasapiOut.Play();
            }
        }

        public void Pause()
        {
            if (_wasapiOut?.PlaybackState == PlaybackState.Playing)
            {
                _wasapiOut?.Pause();
            }
        }
        
        public void Seek(TimeSpan time)
        {
            if (_reader == null)
            {
                return;
            }

            var offset = _reader.WaveFormat.AverageBytesPerSecond * (int)Math.Floor(time.TotalSeconds);
            if (offset > 0 && offset < _reader.Length)
            {
                _reader.Seek(offset, SeekOrigin.Begin);
            }
        }

        public void Stop()
        {
            if (_wasapiOut?.PlaybackState == PlaybackState.Stopped)
            {
                return;
            }

            // Stop playing
            _wasapiOut?.Stop();
            
            // Cleanup
            _wasapiOut?.Dispose();
            _wasapiOut = null;

            // Reset stream
            _reader?.Dispose();
            _reader = null;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
