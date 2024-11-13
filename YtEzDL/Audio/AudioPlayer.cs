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
        private static readonly WaveFormat Format = new WaveFormat(48000, 16, 2);
        public readonly WaveOut WaveOut;
        private FfMpegStream _ffMpegStream;
        private readonly string _url;

        public AudioPlayer(int desiredLatency = 64, int numberOfBuffers = 16, int device = 0)
        {
            // Init device
            WaveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
            WaveOut.DesiredLatency = desiredLatency;
            WaveOut.NumberOfBuffers = numberOfBuffers;
            WaveOut.DeviceNumber = device;
        }
        
        public AudioPlayer(string url, int desiredLatency = 64, int numberOfBuffers = 16, int device = 0) : this(desiredLatency, numberOfBuffers, device)
        {
            _url = url;
        }
        
        public void Play(string url, TimeSpan position)
        {
            if (_ffMpegStream == null)
            {
                // Create ffmpeg stream
                _ffMpegStream = new FfMpegStream(url, position, AudioFormat.S16Le, "-ar", "48000", "-ac", "2");

                // Init stream
                WaveOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));

                // Play
                WaveOut.Play();
            }
            else
            {
                // Pause
                WaveOut.Stop();

                // Dispose stream
                _ffMpegStream.Dispose();

                // Create new stream
                _ffMpegStream = new FfMpegStream(url, position, AudioFormat.S16Le, "-ar", "48000", "-ac", "2");

                // Init stream
                WaveOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));
                
                // Resume
                WaveOut.Play();
            }
        }

        public void Play(string url)
        {
            Play(url, TimeSpan.Zero);
        }

        public void Play(TimeSpan position)
        {
            Play(_url, TimeSpan.Zero);
        }
        
        public void Play()
        {
            Play(TimeSpan.Zero);
        }

        public void Pause() => WaveOut.Pause();
        public void Resume() => WaveOut.Resume();
        public void Stop()
        {
            _ffMpegStream.Dispose();
            _ffMpegStream = null;
            WaveOut.Stop();
        }

        public long Position => WaveOut.GetPosition();
        public PlaybackState PlaybackState => WaveOut.PlaybackState;
        public float Volume
        {
            get => WaveOut.Volume;
            set => WaveOut.Volume = value;
        }
        
        public void Dispose()
        {
            WaveOut.Dispose();
            _ffMpegStream.Dispose();
        }
    }
}
