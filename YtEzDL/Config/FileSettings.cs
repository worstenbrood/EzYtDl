using System;
using Newtonsoft.Json;

namespace YtEzDL.Config
{
    public class FileSettings
    {
        private volatile string _path;

        [JsonProperty(PropertyName = "path")]
        public string Path
        {
            // If not set use executable path
            get => _path ?? (_path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            set => _path = value;
        }

        private volatile bool _createPlaylistFolder;

        [JsonProperty(PropertyName = "create_playlist_folder")]
        public bool CreatePlaylistFolder
        {
            get => _createPlaylistFolder;
            set => _createPlaylistFolder = value;
        }
    }
}