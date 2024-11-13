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
        private static readonly WaveFormat Format = new WaveFormat(44100, 16, 2);
        private readonly WaveOut _waveOut;
        private FfMpegStream _ffMpegStream;
        private string _url;

        public AudioPlayer(int desiredLatency = 64, int numberOfBuffers = 16, int device = 0)
        {
            // Init device
            _waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback())
            {
                DesiredLatency = desiredLatency,
                NumberOfBuffers = numberOfBuffers,
                DeviceNumber = device
            };
        }
        
        public AudioPlayer(string url, int desiredLatency = 60, int numberOfBuffers = 10, int device = 0) : this(desiredLatency, numberOfBuffers, device)
        {
            _url = url;
        }

        private static readonly string[] ExtraArguments = new[]
            { "-ar", "44100", "-ac", "2"};

        public void Play(string url, TimeSpan position)
        {
            if (_ffMpegStream == null)
            {
                // Create ffmpeg stream
                _ffMpegStream = new FfMpegStream(url, position, AudioFormat.S16Le, ExtraArguments);

                // Save url
                _url = url;

                // Init stream
                _waveOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));

                // Play
                _waveOut.Play();
            }
            else
            {
                // Pause
                _waveOut.Stop();

                // Dispose stream
                _ffMpegStream.Dispose();

                // Create new stream
                _ffMpegStream = new FfMpegStream(url, position, AudioFormat.S16Le, ExtraArguments);

                // Save url
                _url = url;

                // Init stream
                _waveOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));
                
                // Resume
                _waveOut.Play();
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
                _waveOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));

                // Play
                _waveOut.Play();
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

        public void Pause() => _waveOut.Pause();
        public void Resume() => _waveOut.Resume();
        public void Stop()
        {
            _ffMpegStream.Dispose();
            _ffMpegStream = null;
            _waveOut.Stop();
        }

        public long Position => _waveOut.GetPosition();
        public PlaybackState PlaybackState => _waveOut.PlaybackState;
        public float Volume
        {
            get => _waveOut.Volume;
            set => _waveOut.Volume = value;
        }
        
        public void Dispose()
        {
            _waveOut.Dispose();
            _ffMpegStream.Dispose();
        }
    }
}
