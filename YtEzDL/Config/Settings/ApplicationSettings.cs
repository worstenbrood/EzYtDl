using Newtonsoft.Json;
using YtEzDL.DownLoad;

namespace YtEzDL.Config.Settings
{
    public class ApplicationSettings
    {
        private volatile bool _autostart;

        [JsonProperty(PropertyName = "autostart")]
        public bool Autostart
        {
            get => _autostart;
            set => _autostart = value;
        }

        private volatile bool _advancedSettings;

        [JsonProperty(PropertyName = "advancedSettings")]
        public bool AdvancedSettings
        {
            get => _advancedSettings;
            set => _advancedSettings = value;
        }
    }
}