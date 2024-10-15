using System;
using System.Drawing;
using System.IO;
using System.Reflection;
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
            get => _path ?? (_path = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName);
            set => _path = value;
        }

        [JsonProperty(PropertyName = "create_playlist_folder")]
        public volatile bool CreatePlaylistFolder;
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

        [JsonProperty(PropertyName = "fetch_thumbnail")]
        public volatile bool FetchThumbnail = true;

        [JsonProperty(PropertyName = "fetch_best_thumbnail")]
        public volatile bool FetchBestThumbnail = true;

        [JsonProperty(PropertyName = "extract_audio")]
        public volatile bool ExtractAudio = true;

        [JsonProperty(PropertyName = "add_metadata")]
        public volatile bool AddMetadata = true;

        [JsonProperty(PropertyName = "embed_thumbnail")]
        public volatile bool EmbedThumbnail = true;

        [JsonProperty(PropertyName = "audio_format")]
        public volatile AudioFormat AudioFormat = AudioFormat.Mp3;

        [JsonProperty(PropertyName = "audio_quality")]
        public volatile AudioQuality AudioQuality = AudioQuality.Cbr320;
        
        [JsonProperty(PropertyName = "video_format")]
        public volatile VideoFormat VideoFormat = VideoFormat.Mkv;
    }

    public class LayoutSettings
    {
        [JsonProperty(PropertyName = "autoselect")]
        public volatile bool AutoSelect = true;

        private readonly LockedProperty<Color> _selectionColor = new LockedProperty<Color>(MetroColors.Blue);

        [JsonProperty(PropertyName = "selectionColor")]
        public Color SelectionColor 
        {
            get => _selectionColor.Get(); 
            set => _selectionColor.Set(value);
        }

        [JsonProperty(PropertyName = "selectionWidth")]
        public volatile float SelectionWidth = 4;
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


        public Configuration(string filename, bool load = true) : base(filename, load)
        {
        }
    }

    public class ConfigurationFile
    {
        private readonly string _filename;
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

            _filename = Path.GetDirectoryName(filename) == string.Empty ? 
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
            lock (_lock)
            {
                try
                {
                    using (var textReader = new StringReader(File.ReadAllText(_filename, Encoding.UTF8)))
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
            lock (_lock)
            {
                using (var textWriter = new JsonTextWriter(new StreamWriter(File.OpenWrite(_filename), Encoding.UTF8)))
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
