using System.Drawing;
using MetroFramework;
using Newtonsoft.Json;

namespace YtEzDL.Config
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

        private readonly LockedProperty<Color> _selectionColor = new LockedProperty<Color>(MetroColors.Blue);

        [JsonProperty(PropertyName = "selectionColor")]
        public Color SelectionColor 
        {
            get => _selectionColor.Get(); 
            set => _selectionColor.Set(value);
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
    }
}