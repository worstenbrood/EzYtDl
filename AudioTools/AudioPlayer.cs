using System;
using System.IO;
using System.Threading;
using AudioTools.Dsp;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using SoundTouch;

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

        public readonly string AudioFile;
        public int Latency;
        public DspProvider Dsp { get; private set; } = new DspProvider();

        private IWavePlayer _wavePlayer;
        private MediaFoundationReader _reader;
        private SoundTouchWaveProvider _waveStream;
        private SoundTouchProcessor _soundTouchProcessor = SoundTouchProcessor.CreateDefault();
        private ManualResetEvent _resetEvent = new ManualResetEvent(false);
        
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
                    double position = _reader.Position;
                    var waveFormat = _waveStream.WaveFormat;
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

        public AudioPlayer(string audioFile, int latency = 20)
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

        public float CalculatedBpm => _waveStream?.Bpm ?? 0;

        protected static readonly MediaFoundationReader.MediaFoundationReaderSettings Settings = new
            MediaFoundationReader.MediaFoundationReaderSettings
            {
                RequestFloatOutput = true,
                RepositionInRead = true,
            };

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
                // Open file
                _reader = new MediaFoundationReader(AudioFile, Settings);
                if (time != null)
                {
                    Seek(time.Value);
                }
                
                // Init soundtouch processor
                _soundTouchProcessor.TempoChange = tempoChange;
                _soundTouchProcessor.RateChange = rateChange;

                // Create SoundTouch stream
                _waveStream = new SoundTouchWaveProvider(_reader, _soundTouchProcessor, true);

                // Append DSP
                Dsp.SetBaseProvider(_waveStream.ToSampleProvider());

                // Open audio device
                _wavePlayer = wavePlayer ?? new WasapiOut(AudioClientShareMode.Exclusive, Latency);
                _wavePlayer.PlaybackStopped += (o, e) => _resetEvent.Set();
                _wavePlayer.Init(Dsp);
                _wavePlayer.Play();
            }
        }

        public bool Wait(int milliseconds = -1)
        {
            if (_wavePlayer?.PlaybackState == PlaybackState.Playing)
            {
                return _resetEvent.WaitOne(milliseconds);
            }

            return true;
        }

        public void Pause()
        {
            if (_wavePlayer?.PlaybackState == PlaybackState.Playing)
            {
                _wavePlayer?.Pause();
            }
        }
        
        public void Seek(TimeSpan time)
        {
            if (_reader == null)
            {
                return;
            }

            // Clear data
            _soundTouchProcessor.Clear();
            
            var offset = (int)Math.Round(_reader.WaveFormat.AverageBytesPerSecond * time.TotalSeconds);
            if (offset > 0 && offset < _reader.Length)
            {
                _reader.Seek(offset, SeekOrigin.Begin);
            }
        }

        private void Cleanup()
        {
            _soundTouchProcessor?.Flush();

            // Cleanup
            _wavePlayer?.Dispose();
            _wavePlayer = null;

            // Reset stream
            _reader?.Dispose();
            _reader = null;
        }

        public void Stop()
        {
            Cleanup();
        }

        public virtual void Dispose()
        {
            Cleanup();

            _soundTouchProcessor?.Dispose();
            _soundTouchProcessor = null;

            _resetEvent?.Dispose();
            _resetEvent = null;
        }
    }
}
