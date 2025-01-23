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

        private WasapiOut _wasapiOut;
        private readonly MediaFoundationReader _reader;
        public DspProvider Dsp { get; private set; }

        public AudioPlayer(string audioFile)
        {
            _reader = new MediaFoundationReader(audioFile);
            Dsp = new DspProvider(_reader.ToSampleProvider());
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
                _wasapiOut = new WasapiOut();
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
            _reader.Seek(0, SeekOrigin.Begin);
        }

        public void Dispose()
        {
            Stop();
            _reader?.Dispose();
        }
    }
}
