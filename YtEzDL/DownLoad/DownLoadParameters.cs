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
        Wav,
        S16Le,
        Nut
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

    public class DownLoadParameters : Parameters<DownLoadParameters>
    {
        public static DownLoadParameters Create => new DownLoadParameters();
        
        public DownLoadParameters RemoveCache()
        {
            return AddParameter("--rm-cache-dir");
        }

        public DownLoadParameters ExtractAudio()
        {
            return AddParameter("-x");
        }

        public DownLoadParameters AddMetadata()
        {
            return AddParameter("--add-metadata");
        }

        public DownLoadParameters EmbedThumbnail()
        {
            return AddParameter("--embed-thumbnail");
        }

        public DownLoadParameters AudioFormat(AudioFormat format)
        {
            return AddParameter("--audio-format", format.ToString("G").ToLowerInvariant());
        }

        public DownLoadParameters AudioQuality(AudioQuality quality)
        {
            return AddParameter("--audio-quality", quality.ToString("D"));
        }

        public DownLoadParameters MetadataFromTitle(string format)
        {
            return AddParameter("--metadata-from-title", format);
        }

        public DownLoadParameters VideoFormat(VideoFormat format)
        {
            return AddParameter("--recode-video", format.ToString("G").ToLowerInvariant());
        }

        public DownLoadParameters IgnoreErrors()
        {
            return AddParameter("--ignore-errors");
        }

        public DownLoadParameters SetPath(string path)
        {
            return AddParameter("-P", path);
        }

        public DownLoadParameters GetJson()
        {
            return AddParameter("-j");
 }

        public DownLoadParameters Update()
        {
            return AddParameter("--update");
        }

        public DownLoadParameters Version()
        {
            return AddParameter("--version");
        }

        internal DownLoadParameters Url(string url)
        {
            return AddParameter($"\"{url}\"");
        }

        public DownLoadParameters FlatPlaylist()
        {
            return AddParameter("--flat-playlist");
        }

        public DownLoadParameters LazyPlayList()
        {
            return AddParameter("--lazy-playlist");
        }

        public DownLoadParameters NoCleanInfoJson()
        {
            return AddParameter("--no-clean-info-json");
        }

        public DownLoadParameters ParseMetadata(string expression)
        {
            return AddParameter("--parse-metadata", expression);
        }

        public DownLoadParameters ReplaceMetadata(params string[] arg)
        {
            return arg.Length == 0 ? this : 
                AddParameter("--replace-in-metadata", 
                    string.Join(" ", arg.Select(a => $"\"{a}\"")), false);
        }

        internal DownLoadParameters FfMpegLocation(string path)
        {
            return AddParameter("--ffmpeg-location", path);
        }

        public DownLoadParameters UpdateTo(UpdateChannel updateChannel)
        {
            return AddParameter("--update-to", updateChannel.ToString("G").ToLowerInvariant());
        }

        internal DownLoadParameters Output(string path)
        {
            return AddParameter("-o", path);
        }
    }
}