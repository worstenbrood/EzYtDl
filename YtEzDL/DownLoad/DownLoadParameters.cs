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

    public enum DownloadAction
    {
        Download,
        ExtractAudio
    }

    public class DownLoadParameters : Parameters
    {
        public static DownLoadParameters Create => new DownLoadParameters();
        
        public DownLoadParameters RemoveCache()
        {
            AddParameter("--rm-cache-dir");
            return this;
        }

        public DownLoadParameters ExtractAudio()
        {
            AddParameter("-x");
            return this;
        }

        public DownLoadParameters AddMetadata()
        {
            AddParameter("--add-metadata");
            return this;
        }

        public DownLoadParameters EmbedThumbnail()
        {
            AddParameter("--embed-thumbnail");
            return this;
        }

        public DownLoadParameters AudioFormat(AudioFormat format)
        {
            AddParameter("--audio-format", format.ToString("G").ToLowerInvariant());
            return this;
        }

        public DownLoadParameters AudioQuality(AudioQuality quality)
        {
            AddParameter("--audio-quality", quality.ToString("D"));
            return this;
        }

        public DownLoadParameters MetadataFromTitle(string format)
        {
            AddParameter("--metadata-from-title", format);
            return this;
        }

        public DownLoadParameters VideoFormat(VideoFormat format)
        {
            AddParameter("--recode-video", format.ToString("G").ToLowerInvariant());
            return this;
        }

        public DownLoadParameters IgnoreErrors()
        {
            AddParameter("--ignore-errors");
            return this;
        }

        public DownLoadParameters SetPath(string path)
        {
            AddParameter("-P", $"\"{path}\"");
            return this;
        }

        public DownLoadParameters GetJson()
        {
            AddParameter("-j");
            return this;
        }

        public DownLoadParameters Update()
        {
            AddParameter("--update");
            return this;
        }

        public DownLoadParameters Version()
        {
            AddParameter("--version");
            return this;
        }

        public DownLoadParameters Url(string url)
        {
            AddParameter($"\"{url}\"");
            return this;
        }

        public DownLoadParameters FlatPlaylist()
        {
            AddParameter("--flat-playlist");
            return this;
        }

        public DownLoadParameters LazyPlayList()
        {
            AddParameter("--lazy-playlist");
            return this;
        }

        public DownLoadParameters NoCleanInfoJson()
        {
            AddParameter("--no-clean-info-json");
            return this;
        }

        public DownLoadParameters ParseMetadata(string expression)
        {
            AddParameter("--parse-metadata", expression);
            return this;
        }

        public DownLoadParameters ReplaceMetadata(string expression)
        {
            AddParameter("--replace-in-metadata", expression);
            return this;
        }

        public DownLoadParameters Reset()
        {
            Clear();
            return this;
        }
    }
}