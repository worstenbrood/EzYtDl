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

        private volatile UpdateChannel _updateChannel = UpdateChannel.Stable;

        [JsonProperty(PropertyName = "updateChannel")]
        public UpdateChannel UpdateChannel
        {
            get => _updateChannel;
            set => _updateChannel = value;
        }
    }
}