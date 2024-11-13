using System;
using NAudio.Wave;
using YtEzDL.DownLoad;
using YtEzDL.Tools;

namespace YtEzDL.Audio
{
    /// <summary>
    /// AudioPlayer that plays any yt-dlp supported url. It transfers yt-dlps output into ffmpeg, converts it to wav, and streams that
    /// to the audio device using NAudio
    /// </summary>
    public class AudioPlayer : IDisposable
    {
        private const int Rate = 44100;
        private const int Bits = 16;
        private const int Channels = 2;
        private static readonly WaveFormat Format = new WaveFormat(Rate, Bits, Channels);
        private readonly WasapiOut _wasapiOut;
        private FfMpegStream _ffMpegStream;
        private string _url;

        public event EventHandler<StoppedEventArgs> PlaybackStopped
        {
            add => _wasapiOut.PlaybackStopped += value;
            remove => _wasapiOut.PlaybackStopped -= value;
        }

        private static readonly Lazy<AudioPlayer> Lazy = new Lazy<AudioPlayer>(() => new AudioPlayer());
        public static AudioPlayer Instance => Lazy.Value;

        private AudioPlayer()
        {
            // Init device
            _wasapiOut = new WasapiOut();
        }
        
        public AudioPlayer(string url) : this()
        {
            _url = url;
        }

        private static readonly string[] ExtraArguments = new[]
            { "-ar", Rate.ToString(), "-ac", Channels.ToString()};

        public void Play(string url, TimeSpan position)
        {
            if (_ffMpegStream == null)
            {
                // Create ffmpeg stream
                _ffMpegStream = new FfMpegStream(url, position, AudioFormat.S16Le, ExtraArguments);

                // Save url
                _url = url;

                // Init stream
                _wasapiOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));

                // Play
                _wasapiOut.Play();
            }
            else
            {
                // Pause
                _wasapiOut.Stop();

                // Dispose stream
                _ffMpegStream.Dispose();

                // Create new stream
                _ffMpegStream = new FfMpegStream(url, position, AudioFormat.S16Le, ExtraArguments);

                // Save url
                _url = url;

                // Init stream
                _wasapiOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));

                // Resume
                _wasapiOut.Play();
            }
        }

        public void Play(string url)
        {
            Play(url, TimeSpan.Zero);
        }

        public void Play(TimeSpan position)
        {
            if (_ffMpegStream == null)
            {
                // Create ffmpeg stream
                _ffMpegStream = new FfMpegStream(_url, position, AudioFormat.S16Le, ExtraArguments);

                // Init stream
                _wasapiOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));

                // Play
                _wasapiOut.Play();
            }
            else
            {
                // Pause
                Pause();

                // Create new writer
                _ffMpegStream.DisposeWriter();
                _ffMpegStream.CreateWriter(_url, position);

                // Resume
                Resume();
            }
        }
        
        public void Play()
        {
            Play(TimeSpan.Zero);
        }

        public void Pause() => _wasapiOut.Pause();
        public void Resume() => _wasapiOut.Play();
        public void Stop()
        {
            _ffMpegStream.Dispose();
            _ffMpegStream = null;
            _wasapiOut.Stop();
        }

        public long Position => _wasapiOut.GetPosition();
        public PlaybackState PlaybackState => _wasapiOut.PlaybackState;
        public float Volume
        {
            get => _wasapiOut.Volume;
            set => _wasapiOut.Volume = value;
        }
        
        public void Dispose()
        {
            _wasapiOut.Dispose();
            _ffMpegStream?.Dispose();
        }
    }
}
