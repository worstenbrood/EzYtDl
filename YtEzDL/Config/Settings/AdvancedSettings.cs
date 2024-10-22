using Newtonsoft.Json;
using YtEzDL.DownLoad;

namespace YtEzDL.Config.Settings
{
    public class AdvancedSettings
    {

        private volatile UpdateChannel _updateChannel = UpdateChannel.Stable;

        [JsonProperty(PropertyName = "updateChannel")]
        public UpdateChannel UpdateChannel
        {
            get => _updateChannel;
            set => _updateChannel = value;
        }
    }
}
