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
        private static readonly object Lock = new object();
        private static Configuration _configuration;

        public static Configuration Default
        {
            get
            {
                lock (Lock)
                {
                    return _configuration ?? (_configuration = new Configuration(DefaultFilename, true, nameof(YtEzDL)));
                }
            }
        }

        public Configuration()
        {
        }

        public Configuration(string filename, bool load = true) : base(filename, load)
        {
        }

        public Configuration(string filename, bool load = true, string subFolder = null) : base(filename, load, subFolder)
        {
        }
    }
}
