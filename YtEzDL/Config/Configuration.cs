using System;
using Newtonsoft.Json;
using YtEzDL.Utils;
using YtEzDL.Config.Settings;

namespace YtEzDL.Config
{
    public class Configuration : JsonFile
    {
        // Config

        [JsonProperty(PropertyName = "fileSettings")]
        public FileSettings FileSettings { get; set; } = new FileSettings();

        [JsonProperty(PropertyName = "downloadSettings")]
        public DownloadSettings DownloadSettings { get; set; } = new DownloadSettings();

        [JsonProperty(PropertyName = "layoutSettings")]
        public LayoutSettings LayoutSettings { get; set; } = new LayoutSettings();

        [JsonProperty(PropertyName = "applicationSettings")]
        public ApplicationSettings ApplicationSettings { get; set; } = new ApplicationSettings();

        [JsonProperty(PropertyName = "advancedSettings")]
        public AdvancedSettings AdvancedSettings { get; set; } = new AdvancedSettings();

        // Logic

        private const string DefaultFilename = "ezytdl.json";
        
        private static readonly Lazy<Configuration> Lazy = new Lazy<Configuration>
            (() => new Configuration(DefaultFilename, CommonTools.ApplicationName));

        public static Configuration Default => Lazy.Value;

        public Configuration()
        {
        }

        public Configuration(string filename, string subFolder = null, bool load = true) : base(filename, load, subFolder)
        {
        }
    }
}
