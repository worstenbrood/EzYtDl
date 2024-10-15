using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace YtEzDL.Utils
{
    public class FileSettings
    {
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }
    }

    public class Configuration : ConfigurationWorker<Configuration>
    {
        // Config

        [JsonProperty(PropertyName = "fileSettings")]
        public FileSettings FileSettings { get; set; } = new FileSettings();

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

    public class ConfigurationWorker<T>
        where T : class
    {
        private readonly ConfigurationFile<T> _configurationFile;

        public ConfigurationWorker(string filename, bool load = true)
        {
            _configurationFile = new ConfigurationFile<T>(filename);

            if (load)
            {
                Load();
            }
        }

        public void Load()
        {
            _configurationFile.Load(this);
        }

        public void Save()
        {
            _configurationFile.Save(this);
        }
    }
    
    public class ConfigurationFile<T>
        where T: class
    {
        private readonly string _filename;
        private readonly object _lock = new object();

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Formatting = Formatting.Indented
        };

        public ConfigurationFile(string filename)
        {
            _filename = Path.GetDirectoryName(filename) == string.Empty ? 
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), filename) : 
                filename;
        }

        public T Load()
        {
            lock (_lock)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(File.ReadAllText(_filename, Encoding.UTF8), _serializerSettings);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public void Load(object configuration)
        {
            lock (_lock)
            {
                try
                {
                    JsonConvert.PopulateObject(File.ReadAllText(_filename, Encoding.UTF8), configuration, _serializerSettings);
                }
                catch (Exception e)
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
