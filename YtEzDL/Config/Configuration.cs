using Newtonsoft.Json;

namespace YtEzDL.Config
{
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
        private static Configuration _configuration;

        public static Configuration Default
        {
            get
            {
                lock (_configuration)
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
}
