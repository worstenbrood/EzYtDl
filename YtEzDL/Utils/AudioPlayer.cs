using System;
using NAudio.Wave;
using YtEzDL.DownLoad;
using YtEzDL.Tools;

namespace YtEzDL.Utils
{
    public class AudioPlayer : IDisposable
    {
        private static readonly WaveFormat Format = new WaveFormat(48000, 16, 2);
        private readonly WaveOut _waveOut;
        private readonly FfMpegStream _ffMpegStream;

        public AudioPlayer(string url)
        {
            // Init device
            _waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
            _waveOut.DesiredLatency = 60;
            _waveOut.NumberOfBuffers = 10;
            _waveOut.PlaybackStopped += (sender, e) =>
            {
                _waveOut.Stop();
            };

            // Create ffmpeg stream
            _ffMpegStream = new FfMpegStream(url, AudioFormat.Wav);

            // Init stream
            _waveOut.Init(new RawSourceWaveStream(_ffMpegStream, Format));
        }

        public void Play() => _waveOut.Play();
        public void Pause() => _waveOut.Pause();
        public void Resume() => _waveOut.Resume();
        public void Stop() => _waveOut.Stop();
        public long Position => _waveOut.GetPosition();
        public PlaybackState PlaybackState => _waveOut.PlaybackState;
        public float Volume
        {
            get => _waveOut.Volume;
            set => _waveOut.Volume = value;
        }
        
        public void Dispose()
        {
            _waveOut?.Dispose();
            _ffMpegStream?.Dispose();
        }
    }
}
