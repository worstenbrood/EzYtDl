using System;
using NAudio.Wave;
using YtEzDL.DownLoad;
using YtEzDL.Tools;

namespace YtEzDL.Utils
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

        public AudioPlayer(string url, int desiredLatency = 300, int numberOfBuffers = 10, int device = 0)
        {
            _url = url;

            // Init device
            WaveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
            WaveOut.DesiredLatency = desiredLatency;
            WaveOut.NumberOfBuffers = numberOfBuffers;
            WaveOut.DeviceNumber = device;
            WaveOut.PlaybackStopped += (sender, e) =>
            {
                WaveOut.Stop();
            };
        }

        public void Play()
        {
            if (_ffMpegStream == null)
            {
                // Create ffmpeg stream
                _ffMpegStream = new FfMpegStream(_url, AudioFormat.Wav);

                // Init stream
                WaveOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));
            }

            WaveOut.Play();
        }

        public void Pause() => WaveOut.Pause();
        public void Resume() => WaveOut.Resume();
        public void Stop() => WaveOut.Stop();
        public long Position => WaveOut.GetPosition();
        public PlaybackState PlaybackState => WaveOut.PlaybackState;
        public float Volume
        {
            get => WaveOut.Volume;
            set => WaveOut.Volume = value;
        }
        
        public void Dispose()
        {
            WaveOut?.Dispose();
            _ffMpegStream?.Dispose();
        }
    }
}
