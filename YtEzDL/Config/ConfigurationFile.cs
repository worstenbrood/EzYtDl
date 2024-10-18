using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YtEzDL.Utils;

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

        public ConfigurationFile() : this(null, false, null)
        {
        }

        public ConfigurationFile(string filename, string subfolder) : this(filename, true, subfolder)
        {
        }

        public ConfigurationFile(string filename, bool load = true, string subfolder = null)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return;
            }

            if (Path.GetDirectoryName(filename) == string.Empty)
            {
                var profile = Tools.ProfileFolderCombine(filename);
                if (!string.IsNullOrEmpty(subfolder))
                {
                    var folderName = Tools.ProfileFolderCombine(subfolder);
                    Directory.CreateDirectory(folderName);
                    var profileFolder = Path.Combine(folderName, filename);

                    if (File.Exists(profile))
                    {
                        File.Move(profile, profileFolder);
                    }

                    _filename = profileFolder;
                }
                else
                {
                    _filename = profile;
                }
            }
            else
            {
                _filename = filename;
            }

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