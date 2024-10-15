using System;
using System.Drawing;
using System.IO;
using System.Text;
using MetroFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YtEzDL.Utils
{
    public class FileSettings
    {
        private volatile string _path;

        [JsonProperty(PropertyName = "path")]
        public string Path
        {
            // If not set use executable path
            get => _path ?? (_path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            set => _path = value;
        }

        private volatile bool _createPlaylistFolder;

        [JsonProperty(PropertyName = "create_playlist_folder")]
        public bool CreatePlaylistFolder
        {
            get => _createPlaylistFolder;
            set => _createPlaylistFolder = value;
        }
    }

    public class DownloadSettings
    {
        private volatile int _downloadThreads;

        [JsonProperty(PropertyName = "download_threads")]
        public int DownloadThreads
        {
            // Minimum 1 thread
            get => _downloadThreads <= 0 ? _downloadThreads = 2 : _downloadThreads;
            set => _downloadThreads = value;
        }

        private volatile bool _fetchThumbnail = true;

        [JsonProperty(PropertyName = "fetch_thumbnail")]
        public bool FetchThumbnail
        {
            get => _fetchThumbnail;
            set => _fetchThumbnail = value;
        }

        private volatile bool _fetchBestThumbnail = true;

        [JsonProperty(PropertyName = "fetch_best_thumbnail")]
        public bool FetchBestThumbnail
        {
            get => _fetchBestThumbnail;
            set => _fetchBestThumbnail = value;
        }

        private volatile bool _extractAudio = true;

        [JsonProperty(PropertyName = "extract_audio")]
        public bool ExtractAudio
        {
            get => _extractAudio;
            set => _extractAudio = value;
        }

        private volatile bool _addMetadata = true;

        [JsonProperty(PropertyName = "add_metadata")]
        public bool AddMetadata 
        {
            get => _addMetadata;
            set => _addMetadata = value;
        }

        private volatile bool _embedThumbnail = true;

        [JsonProperty(PropertyName = "embed_thumbnail")]
        public bool EmbedThumbnail
        {
            get => _embedThumbnail;
            set => _embedThumbnail = value;
        }

        private volatile AudioFormat _audioFormat = AudioFormat.Mp3;

        [JsonProperty(PropertyName = "audio_format")]
        public AudioFormat AudioFormat
        {
            get => _audioFormat;
            set => _audioFormat = value;
        }
        
        private volatile AudioQuality _audioQuality = AudioQuality.Cbr320;
        
        [JsonProperty(PropertyName = "audio_quality")]
        public AudioQuality AudioQuality
        {
            get => _audioQuality;
            set => _audioQuality = value;
        }

        private volatile VideoFormat _videoFormat = VideoFormat.Mkv;

        [JsonProperty(PropertyName = "video_format")]
        public VideoFormat VideoFormat
        {
            get => _videoFormat;
            set => _videoFormat = value;
        }
    }

    public class LayoutSettings
    {
        private volatile bool _autoSelect = true;

        [JsonProperty(PropertyName = "autoselect")]
        public bool AutoSelect
        {
            get => _autoSelect;
            set => _autoSelect = value;
        }

        private readonly LockedProperty<Color> _selectionColor = new LockedProperty<Color>(MetroColors.Blue);

        [JsonProperty(PropertyName = "selectionColor")]
        public Color SelectionColor 
        {
            get => _selectionColor.Get(); 
            set => _selectionColor.Set(value);
        }

        private volatile float _selectionWidth = 4;

        [JsonProperty(PropertyName = "selectionWidth")]
        public float SelectionWidth
        {
            get => _selectionWidth;
            set => _selectionWidth = value;
        }
    }

    public class Configuration : ConfigurationFile
    {
        // Config

        [JsonProperty(PropertyName = "fileSettings")]
        public FileSettings FileSettings { get; set; } = new FileSettings();

        [JsonProperty(PropertyName = "downloadSettings")]
        public DownloadSettings DownloadSettings { get; set; } = new DownloadSettings();

        [JsonProperty(PropertyName = "layoutSettings")]
        public LayoutSettings LayoutSettings { get; set; } = new LayoutSettings();

        // Logic

        private const string DefaultFilename = "ezytdl.json";
        private static readonly object Lock = new object();
        private static Configuration _configuration;

        public static Configuration Default
        {
            get
            {
                lock (Lock)
                {
                    return _configuration ?? (_configuration = new Configuration(DefaultFilename));
                }
            }
        }

        public Configuration() : base(null, false)
        {
        }

        public Configuration(string filename, bool load = true) : base(filename, load)
        {
        }
    }

    public class ConfigurationFile
    {
        [JsonIgnore]
        public readonly string Filename;
        private readonly object _lock = new object();
        
        private static readonly JsonSerializer JsonSerializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        public ConfigurationFile(string filename, bool load = true)
        {
            JsonSerializer.Converters.Add(new StringEnumConverter());

            Filename = Path.GetDirectoryName(filename) == string.Empty ? 
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), filename) : 
                filename;

            if (load)
            {
                Load();
            }
        }
        
        public void Load()
        {
            Load(this);
        }

        public void Save()
        {
            Save(this);
        }

        public void Load(object configuration)
        {
            if (Filename == null)
            {
                throw new ArgumentNullException(nameof(Filename));
            }

            lock (_lock)
            {
                try
                {
                    using (var textReader = new StreamReader(File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.UTF8))
                    {
                        JsonSerializer.Populate(textReader, configuration);
                    }
                }
                catch (Exception)
                {
                    // Ignore
                }
            }
        }

        public void Save(object configuration)
        {
            if (Filename == null)
            {
                throw new ArgumentNullException(nameof(Filename));
            }

            lock (_lock)
            {
                using (var textWriter = new JsonTextWriter(new StreamWriter(File.Open(Filename, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.UTF8)))
                {
                    JsonSerializer.Serialize(textWriter, configuration);
                }
            }
        }
    }

    public class LockedProperty<T>
    {
        private readonly object _lock = new object();
        private T _value;

        public LockedProperty(T @default)
        {
            _value = @default;
        }

        public void Set(T value)
        {
            lock (_lock)
            {
                _value = value;
            }
        }

        public T Get()
        {
            lock (_lock)
            {
                return _value;
            }
        }
    }
}
