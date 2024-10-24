using System.Linq;

namespace YtEzDL.DownLoad
{
    public enum AudioFormat
    {
        Best,
        Aac,
        Flac,
        Mp3,
        M4A,
        Opus,
        Vorbis,
        Wav
    }

    public enum AudioQuality
    {
        Best = 0,
        Medium = 1,
        Worst = 9,
        Cbr128 = 128,
        Cbr192 = 192,
        Cbr256 = 256,
        Cbr320 = 320
    }

    public enum VideoFormat
    {
        Mp4,
        Flv,
        Ogg,
        Webm,
        Mkv,
        Avi
    }

    public enum UpdateChannel
    {
        Stable,
        Master,
        Nightly
    }

    public class DownLoadParameters : Parameters
    {
        public static DownLoadParameters Create => new DownLoadParameters();
        
        public DownLoadParameters RemoveCache()
        {
            return AddParameter<DownLoadParameters>("--rm-cache-dir");
        }

        public DownLoadParameters ExtractAudio()
        {
            return AddParameter<DownLoadParameters>("-x");
        }

        public DownLoadParameters AddMetadata()
        {
            return AddParameter<DownLoadParameters>("--add-metadata");
        }

        public DownLoadParameters EmbedThumbnail()
        {
            return AddParameter<DownLoadParameters>("--embed-thumbnail");
        }

        public DownLoadParameters AudioFormat(AudioFormat format)
        {
            return AddParameter<DownLoadParameters>("--audio-format", format.ToString("G").ToLowerInvariant());
        }

        public DownLoadParameters AudioQuality(AudioQuality quality)
        {
            return AddParameter<DownLoadParameters>("--audio-quality", quality.ToString("D"));
        }

        public DownLoadParameters MetadataFromTitle(string format)
        {
            return AddParameter<DownLoadParameters>("--metadata-from-title", format);
        }

        public DownLoadParameters VideoFormat(VideoFormat format)
        {
            return AddParameter<DownLoadParameters>("--recode-video", format.ToString("G").ToLowerInvariant());
        }

        public DownLoadParameters IgnoreErrors()
        {
            return AddParameter<DownLoadParameters>("--ignore-errors");
        }

        public DownLoadParameters SetPath(string path)
        {
            return AddParameter<DownLoadParameters>("-P", path);
        }

        public DownLoadParameters GetJson()
        {
            return AddParameter<DownLoadParameters>("-j");
 }

        public DownLoadParameters Update()
        {
            return AddParameter<DownLoadParameters>("--update");
        }

        public DownLoadParameters Version()
        {
            return AddParameter<DownLoadParameters>("--version");
        }

        internal DownLoadParameters Url(string url)
        {
            return AddParameter<DownLoadParameters>($"\"{url}\"");
        }

        public DownLoadParameters FlatPlaylist()
        {
            return AddParameter<DownLoadParameters>("--flat-playlist");
        }

        public DownLoadParameters LazyPlayList()
        {
            return AddParameter<DownLoadParameters>("--lazy-playlist");
        }

        public DownLoadParameters NoCleanInfoJson()
        {
            return AddParameter<DownLoadParameters>("--no-clean-info-json");
        }

        public DownLoadParameters ParseMetadata(string expression)
        {
            return AddParameter<DownLoadParameters>("--parse-metadata", expression);
        }

        public DownLoadParameters ReplaceMetadata(params string[] arg)
        {
            return arg.Length == 0 ? this : 
                AddParameter<DownLoadParameters>("--replace-in-metadata", 
                    string.Join(" ", arg.Select(a => $"\"{a}\"")), false);
        }

        internal DownLoadParameters FfMpegLocation(string path)
        {
            return AddParameter<DownLoadParameters>("--ffmpeg-location", path);
        }

        public DownLoadParameters UpdateTo(UpdateChannel updateChannel)
        {
            return AddParameter<DownLoadParameters>("--update-to", updateChannel.ToString("G").ToLowerInvariant());
        }

        public DownLoadParameters Reset()
        {
            return Reset<DownLoadParameters>();
        }
    }
}