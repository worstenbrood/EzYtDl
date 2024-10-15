using Newtonsoft.Json;
using YtEzDL.Utils;

namespace YtEzDL.Config
{
    public class DownloadSettings
    {
        private volatile int _downloadThreads;

        [JsonProperty(PropertyName = "download_threads")]
        public int DownloadThreads
        {
            // Minimum 1 thread
            get => _downloadThreads <= 0 ? _downloadThreads = 2 : _downloadThreads;
            set => _downloadThreads = value;
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

        private volatile bool _extractAudio = true;

        [JsonProperty(PropertyName = "extract_audio")]
        public bool ExtractAudio
        {
            get => _extractAudio;
            set => _extractAudio = value;
        }

        private volatile bool _addMetadata = true;

        [JsonProperty(PropertyName = "add_metadata")]
        public bool AddMetadata 
        {
            get => _addMetadata;
            set => _addMetadata = value;
        }

        private volatile bool _embedThumbnail = true;

        [JsonProperty(PropertyName = "embed_thumbnail")]
        public bool EmbedThumbnail
        {
            get => _embedThumbnail;
            set => _embedThumbnail = value;
        }

        private volatile AudioFormat _audioFormat = AudioFormat.Mp3;

        [JsonProperty(PropertyName = "audio_format")]
        public AudioFormat AudioFormat
        {
            get => _audioFormat;
            set => _audioFormat = value;
        }
        
        private volatile AudioQuality _audioQuality = AudioQuality.Cbr320;
        
        [JsonProperty(PropertyName = "audio_quality")]
        public AudioQuality AudioQuality
        {
            get => _audioQuality;
            set => _audioQuality = value;
        }

        private volatile VideoFormat _videoFormat = VideoFormat.Mkv;

        [JsonProperty(PropertyName = "video_format")]
        public VideoFormat VideoFormat
        {
            get => _videoFormat;
            set => _videoFormat = value;
        }
    }
}