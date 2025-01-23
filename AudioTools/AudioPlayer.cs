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

        private WasapiOut _wasapiOut;
        private MediaFoundationReader _reader;
        private SoundTouchWaveProvider _waveStream;
        private SoundTouchProcessor _processor;
        
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

        private TimeSpan _time;

        public TimeSpan Time
        {
            get
            {
                if (_time == TimeSpan.Zero)
                {
                    SetMediaProperties();
                }

                return _time;
            }
        }

        private void SetMediaProperties()
        {
            using (var reader = new MediaFoundationReader(AudioFile))
            {
                _waveFormat = reader.WaveFormat;
                _lengthInBytes = reader.Length;
                _time = TimeSpan.FromSeconds((double)_lengthInBytes / _waveFormat.AverageBytesPerSecond);
            }
        }

        public AudioPlayer(string audioFile, int latency = 200)
        {
            AudioFile = audioFile;
            Latency = latency;
        }
        
        public double TempoChange
        {
            get => _processor?.TempoChange ?? 0;
            set
            {
                if (_processor != null)
                {
                    _processor.TempoChange = value;
                }
            }
        }

        public double RateChange
        {
            get => _processor?.RateChange ?? 0.0D;
            set
            {
                if (_processor != null)
                {
                    _processor.RateChange = value;
                }
            }
        }

        public double PitchOctaves
        {
            get => _processor?.PitchOctaves ?? 0.0D;
            set
            {
                if (_processor != null)
                {
                    _processor.PitchOctaves = value;
                }
            }
        }

        public double PitchSemiTones
        {
            get => _processor?.PitchSemiTones ?? 0.0D;
            set
            {
                if (_waveStream != null)
                {
                    _processor.PitchSemiTones = value;
                }
            }
        }

        public void Play(double tempoChange = 0.0F)
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
                // Open file
                _reader = new MediaFoundationReader(AudioFile);

                // Init processor
                _processor = SoundTouchWaveProvider.CreateDefaultProcessor(_reader);
                _processor.TempoChange = tempoChange;
                
                // Create SoundTouch stream
                _waveStream = new SoundTouchWaveProvider(_reader, _processor);
                Dsp.SetBaseProvider(_waveStream.ToSampleProvider());

                // Open audio device
                _wasapiOut = new WasapiOut(AudioClientShareMode.Exclusive, true, Latency);
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
            // Cleanup
            _wasapiOut?.Dispose();
            _wasapiOut = null;

            // Reset stream
            _reader?.Dispose();
            _reader = null;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
