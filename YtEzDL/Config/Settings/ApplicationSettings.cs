using Newtonsoft.Json;

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

        private volatile bool _captureClipboard = true;

        [JsonProperty(PropertyName = "captureClipboard")]
        public bool CaptureClipboard
        {
            get => _captureClipboard;
            set => _captureClipboard = value;
        }

        private volatile bool _enableHistory = true;

        [JsonProperty(PropertyName = "enableHistory")]
        public bool EnableHistory
        {
            get => _enableHistory;
            set => _enableHistory = value;
        }
    }
}