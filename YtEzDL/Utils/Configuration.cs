using System;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace YtEzDL.Utils
{
    public class FileSettings
    {
        private string _path;

        [JsonProperty(PropertyName = "path")]
        public string Path
        {
            // If not set use executable path
            get => _path ?? (_path = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName);
            set => _path = value;
        }

        [JsonProperty(PropertyName = "create_playlist_folder")]
        public bool CreatePlaylistFolder { get; set; }
    }

    public class DownloadSettings
    {
        private int _downloadThreads;

        [JsonProperty(PropertyName = "download_threads")]
        public int DownloadThreads
        {
            // Minimum 1 thread
            get => _downloadThreads <= 0 ? _downloadThreads = 2 : _downloadThreads;
            set => _downloadThreads = value;
        }

        [JsonProperty(PropertyName = "fetch_best_thumbnail")]
        public bool FetchBestThumbnail { get; set; } = true;
    }

    public class Configuration : ConfigurationFile
    {
        // Config

        [JsonProperty(PropertyName = "fileSettings")]
        public FileSettings FileSettings { get; set; } = new FileSettings();

        [JsonProperty(PropertyName = "downloadSettings")]
        public DownloadSettings DownloadSettings { get; set; } = new DownloadSettings();

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

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Formatting = Formatting.Indented
        };

        public ConfigurationFile(string filename, bool load = true)
        {
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
                    JsonConvert.PopulateObject(File.ReadAllText(_filename, Encoding.UTF8), configuration, _serializerSettings);
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
                File.WriteAllText(_filename, JsonConvert.SerializeObject(configuration, _serializerSettings), Encoding.UTF8);
            }
        }
    }
}
