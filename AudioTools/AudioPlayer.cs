using System;
using System.IO;
using NAudio.Wave;
using System.Threading;

namespace AudioTools
{
    public class AudioPlayer : IDisposable
    {
        private readonly string _audioFile;

        public AudioPlayer(string audioFile)
        {
            _audioFile = audioFile;
        }

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

        private WasapiOut _wasapiOut;
        private MediaFoundationReader _reader;

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
                _reader = new MediaFoundationReader(_audioFile);
                _wasapiOut = new WasapiOut();
                _wasapiOut.Init(_reader.ToSampleProvider());
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
            if (offset < _reader.Length)
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

            _reader?.Dispose();
            _reader = null;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
