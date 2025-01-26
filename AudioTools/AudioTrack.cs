using System;
using System.Threading;
using AudioTools.Dsp;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using SoundTouch;

namespace AudioTools
{
    public class AudioTrack : IDisposable
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

        public readonly string AudioFile;
        public int Latency;
        public DspSampleProvider Dsp { get; private set; } = new DspSampleProvider();

        private readonly SoundTouchProcessor _soundTouchProcessor = SoundTouchProcessor.CreateDefault();
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private IWavePlayer _wavePlayer;
        private AudioSampleProvider _audioSampleProvider;
        
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

        private TimeSpan _totalTime;

        public TimeSpan TotalTime
        {
            get
            {
                if (_totalTime == TimeSpan.Zero)
                {
                    SetMediaProperties();
                }

                return _totalTime;
            }
        }

        public TimeSpan CurrentTime
        {
            get 
            {
                if (_wavePlayer?.PlaybackState == PlaybackState.Playing)
                {
                    double position = _audioSampleProvider.Position;
                    var waveFormat = _audioSampleProvider.WaveFormat;
                    var averageBytesPerSecond = waveFormat.AverageBytesPerSecond;
                    double latency = waveFormat.ConvertLatencyToByteSize(Latency);
                    
                    return TimeSpan.FromSeconds(position  / averageBytesPerSecond - latency / averageBytesPerSecond);
                }

                return TimeSpan.Zero;
            }
        }

        private void SetMediaProperties()
        {
            using (var reader = new MediaFoundationReader(AudioFile))
            {
                _waveFormat = reader.WaveFormat;
                _lengthInBytes = reader.Length;
                _totalTime = TimeSpan.FromSeconds((double)_lengthInBytes / _waveFormat.AverageBytesPerSecond);
            }
        }

        public AudioTrack(string audioFile, int latency = 50)
        {
            AudioFile = audioFile;
            Latency = latency;
        }

        public float Tempo
        {
            set => _soundTouchProcessor.Tempo = value;
        }

        public float TempoChange
        {
            set => _soundTouchProcessor.TempoChange = value;
        }

        public float RateChange
        {
            set => _soundTouchProcessor.RateChange = value;
        }

        public float Pitch
        {
            set => _soundTouchProcessor.Pitch = value;
        }

        public float PitchOctaves
        {
            set => _soundTouchProcessor.PitchOctaves = value;
        }

        public float PitchSemiTones
        {
            set => _soundTouchProcessor.PitchSemiTones = value;
        }

        public float Volume
        {
            get => _wavePlayer?.Volume ?? 0;
            set
            {
                if (_wavePlayer != null)
                {
                    _wavePlayer.Volume = value;
                }
            }
        }

        public PlaybackState PlaybackState => _wavePlayer?.PlaybackState ?? PlaybackState.Stopped;

        public float CalculatedBpm => _audioSampleProvider?.Bpm ?? 0f;
        
        public void Play(float tempoChange = 0.0F, float rateChange = 0.0F, TimeSpan? time = null, IWavePlayer wavePlayer = null)
        {
            if (_wavePlayer != null)
            {
                switch (_wavePlayer.PlaybackState)
                {
                    case PlaybackState.Paused:
                        _wavePlayer.Play();
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
                // Init SoundTouch processor
                _soundTouchProcessor.TempoChange = tempoChange;
                _soundTouchProcessor.RateChange = rateChange;

                // Create SoundTouch stream
                _audioSampleProvider = new AudioSampleProvider(AudioFile, _soundTouchProcessor, true);
                if (time != null)
                {
                    Seek(time.Value);
                }

                // Append DSP
                Dsp.SetInput(_audioSampleProvider);

                // MediaFoundationReader ==> AudioSampleProvider ==> DspProvider ==> Mixer?

                // Open audio device
                _wavePlayer = wavePlayer ?? new WasapiOut(AudioClientShareMode.Exclusive, Latency);
                _wavePlayer.PlaybackStopped += (o, e) => _resetEvent.Set();
                _wavePlayer.Init(Dsp);
                _wavePlayer.Play();
            }
        }

        public bool Wait(int milliseconds = -1)
        {
            return PlaybackState != PlaybackState.Playing || _resetEvent.WaitOne(milliseconds);
        }

        public void Pause()
        {
            if (PlaybackState == PlaybackState.Playing)
            {
                _wavePlayer?.Pause();
            }
        }
        
        public void Seek(TimeSpan time)
        {
            if (_audioSampleProvider == null)
            {
                return;
            }

            // Set position
            _audioSampleProvider.Position = (long)time.TotalSeconds;
        }

        private void Cleanup()
        {
            lock (this)
            {
                _soundTouchProcessor?.Flush();
                // Cleanup
                _wavePlayer?.Dispose();
                // Reset stream
                _audioSampleProvider?.Dispose();
            }
        }

        public void Stop()
        {
            Cleanup();
        }

        public virtual void Dispose()
        {
            Cleanup();

            _soundTouchProcessor?.Dispose();
            _resetEvent?.Dispose();
        }
    }
}
