using Newtonsoft.Json;

namespace YtEzDL.Config
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
    }
}