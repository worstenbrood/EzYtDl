using Newtonsoft.Json;
using YtEzDL.Utils;

namespace YtEzDL.Config.Settings
{
    public class ApplicationSettings
    {
        private volatile bool _autostart;

        [JsonProperty(PropertyName = "autostart")]
        public bool Autostart
        {
            get => _autostart;
            set
            {
                _autostart = value;
                
                // Set auto start
                CommonTools.SetAutoStart(value);
            }
        }
    }
}