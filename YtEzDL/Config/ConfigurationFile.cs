using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YtEzDL.Config
{
    public class ConfigurationFile
    {
        private readonly string _filename;
        private readonly object _lock = new object();
        
        private static readonly JsonSerializer JsonSerializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters = { new StringEnumConverter() }
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
        
        public void Load(object configuration)
        {
            lock (_lock)
            {
                Load(_filename, configuration);
            }
        }

        public static void Load(string filename, object configuration)
        {
            if (filename == null)
            {
                throw new ArgumentNullException(nameof(filename));
            }
            
            try
            {
                using (var textReader = new StreamReader(File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.UTF8))
                {
                    JsonSerializer.Populate(textReader, configuration);
                }
            }
            catch (Exception)
            {
                // Ignore
            }
        }

        public void Save()
        {
            Save(this);
        }

        public void Save(object configuration)
        {
            lock (_lock)
            {
                Save(configuration, _filename);
            }
        }

        public static void Save(object configuration, string filename)
        {
            if (filename == null)
            {
                throw new ArgumentNullException(nameof(filename));
            }
            
            using (var textWriter = new JsonTextWriter(new StreamWriter(File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.UTF8)))
            {
                JsonSerializer.Serialize(textWriter, configuration);
            }
        }
    }
}