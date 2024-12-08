using System;
using NAudio.Wave;
using YtEzDL.DownLoad;
using YtEzDL.Streams;

namespace YtEzDL.Audio
{
    /// <summary>
    /// AudioPlayer that plays any yt-dlp supported url. It transfers yt-dlps output into ffmpeg, converts it to wav, and streams that
    /// to the audio device using NAudio
    /// </summary>
    public class AudioPlayer : IDisposable
    {
        public const int Rate = 44100;
        public const int Bits = 16;
        public const int Channels = 2;
        public static readonly WaveFormat Format = new WaveFormat(Rate, Bits, Channels);
        private WasapiOut _wasapiOut;
        private FfMpegStream _ffMpegStream;
        private readonly object _lock = new object();
        private string _url;

        public event EventHandler<StoppedEventArgs> PlaybackStopped;
        public event ReadEventHandler StreamRead;
        
        public AudioPlayer()
        {
        }
        
        public AudioPlayer(string url) : this()
        {
            _url = url;
        }

        private static readonly string[] ExtraArguments = new[]
            { "-ar", Rate.ToString(), "-ac", Channels.ToString()};

        private void CreateFfMpegStream(string url, TimeSpan position)
        {
            if (_ffMpegStream != null)
            {
                // Dispose stream
                DestroyStream();
            }

            // Create ffmpeg stream
            _ffMpegStream = new FfMpegStream(url, position, AudioFormat.S16Le, ExtraArguments);
            _ffMpegStream.ReadEvent += StreamRead;
        }

        private void DestroyStream()
        {
            if (_ffMpegStream == null)
            {
                return;
            }

            // Dispose stream
            _ffMpegStream.Dispose();
            _ffMpegStream.ReadEvent -= StreamRead;
            _ffMpegStream = null;
        }

        private void CreateWasapiOut()
        {
            _wasapiOut = new WasapiOut();
            _wasapiOut.PlaybackStopped += PlaybackStopped;
        }

        private void DestroyWasapiOut()
        {
             if (_wasapiOut != null)
             {
                 _wasapiOut.Dispose();
                 _wasapiOut.PlaybackStopped -= PlaybackStopped;
                 _wasapiOut = null;
             }
        }

        public void Play(string url, TimeSpan position)
        {
            lock (_lock)
            {
                DestroyWasapiOut();
                CreateWasapiOut();
                DestroyStream();
                CreateFfMpegStream(url, position);

                _url = url;
                _wasapiOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));
                _wasapiOut.Play();
            }
        }

        public void Play(string url)
        {
            Play(url, TimeSpan.Zero);
        }

        public void Play(TimeSpan position)
        {
            lock (_lock)
            {
                CreateWasapiOut();

                if (_ffMpegStream == null)
                {
                    // Create ffmpeg stream
                    CreateFfMpegStream(_url, position);

                    // Init stream
                    _wasapiOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));
                }
                else
                {
                    // Pause
                    _wasapiOut.Pause();

                    // Create new writer
                    _ffMpegStream.DisposeWriter();
                    _ffMpegStream.CreateWriter(_url, position);
                }

                _wasapiOut.Play();
            }
        }
        
        public void Play()
        {
            Play(TimeSpan.Zero);
        }

        public void Pause()
        {
            lock (_lock)
            {
                _wasapiOut?.Pause();
            }
        }

        public void Resume()
        {
            lock (_lock)
            {
                _wasapiOut?.Play();
            }
        }

        public void Reset()
        {
            lock (_lock)
            {
                DestroyStream();
                DestroyWasapiOut();
            }
        }

        public long Position
        {
            get
            {
                lock (_lock)
                {
                    return _wasapiOut.GetPosition();
                }
            }
        }

        public PlaybackState PlaybackState
        {
            get
            {
                lock (_lock)
                {
                    return _wasapiOut?.PlaybackState ?? PlaybackState.Stopped;
                }
            }
        }

        public float Volume
        {
            get
            {
                lock (_lock)
                {
                    return _wasapiOut.Volume;
                }
            }
            set
            {
                lock (_lock)
                {
                    _wasapiOut.Volume = value;
                }
            }
        }

        public void Dispose()
        {
            Reset();
        }
    }
}
