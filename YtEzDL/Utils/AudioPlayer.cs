using System;
using NAudio.Wave;
using YtEzDL.DownLoad;
using YtEzDL.Tools;

namespace YtEzDL.Utils
{
    public class AudioPlayer : IDisposable
    {
        private static readonly WaveFormat Format = new WaveFormat(48000, 16, 2);
        public readonly WaveOut WaveOut;
        private readonly FfMpegStream _ffMpegStream;

        public AudioPlayer(string url, int desiredLatency = 60, int numberOfBuffers = 10)
        {
            // Init device
            WaveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
            WaveOut.DesiredLatency = desiredLatency;
            WaveOut.NumberOfBuffers = numberOfBuffers;
            WaveOut.PlaybackStopped += (sender, e) =>
            {
                WaveOut.Stop();
            };

            // Create ffmpeg stream
            _ffMpegStream = new FfMpegStream(url, AudioFormat.Wav);

            // Init stream
            WaveOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));
        }

        public void Play() => WaveOut.Play();
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
