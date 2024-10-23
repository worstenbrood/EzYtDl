using System.Drawing;
using System.Windows.Forms;
using MetroFramework;
using Newtonsoft.Json;

namespace YtEzDL.Config.Settings
{
    public class LayoutSettings
    {
        private volatile bool _autoSelect = true;

        [JsonProperty(PropertyName = "autoselect")]
        public bool AutoSelect
        {
            get => _autoSelect;
            set => _autoSelect = value;
        }

        private volatile float _selectionWidth = 4;

        [JsonProperty(PropertyName = "selectionWidth")]
        public float SelectionWidth
        {
            get => _selectionWidth;
            set => _selectionWidth = value;
        }

        private volatile bool _perTrackSettings;

        [JsonProperty(PropertyName = "per_track_settings")]
        public bool PerTrackSettings
        {
            get => _perTrackSettings;
            set => _perTrackSettings = value;
        }

        private volatile bool _fetchThumbnail = true;

        [JsonProperty(PropertyName = "fetch_thumbnail")]
        public bool FetchThumbnail
        {
            get => _fetchThumbnail;
            set => _fetchThumbnail = value;
        }

        private volatile bool _fetchBestThumbnail = true;

        [JsonProperty(PropertyName = "fetch_best_thumbnail")]
        public bool FetchBestThumbnail
        {
            get => _fetchBestThumbnail;
            set => _fetchBestThumbnail = value;
        }

        private volatile bool _youtubeFastFetch = true;

        [JsonProperty(PropertyName = "youtube_fast_fetch")]
        public bool YoutubeFastFetch
        {
            get => _youtubeFastFetch;
            set => _youtubeFastFetch = value;
        }

        private volatile MetroColorStyle _colorStyle = MetroColorStyle.Blue;

        [JsonProperty(PropertyName = "colorStyle")]
        public MetroColorStyle ColorStyle
        {
            get => _colorStyle;
            set => _colorStyle = value;
        }

        private volatile FormWindowState _windowState = FormWindowState.Normal;

        [JsonProperty(PropertyName = "windowState")]
        public FormWindowState WindowState
        {
            get => _windowState;
            set => _windowState = value;
        }

        private readonly LockedProperty<Size> _windowSize = new LockedProperty<Size>(new Size(909, 432));

        [JsonProperty(PropertyName = "windowSize")]
        public Size WindowSize
        {
            get => _windowSize.Get();
            set => _windowSize.Set(value);
        }
    }
}