using System.Collections.Generic;

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

    public class DownLoadParameters : Dictionary<string, string>
    {
        public static DownLoadParameters Create => new DownLoadParameters();

        private DownLoadParameters Add(string key, string value = null)
        {
            this[key] = value;
            return this;
        }

        public DownLoadParameters RemoveCache()
        {
            return Add("--rm-cache-dir");
        }

        public DownLoadParameters ExtractAudio()
        {
            return Add("-x");
        }

        public DownLoadParameters AddMetadata()
        {
            return Add("--add-metadata");
        }

        public DownLoadParameters EmbedThumbnail()
        {
            return Add("--embed-thumbnail");
        }

        public DownLoadParameters AudioFormat(AudioFormat format)
        {
            return Add("--audio-format", format.ToString("G").ToLowerInvariant());
        }

        public DownLoadParameters AudioQuality(AudioQuality quality)
        {
            return Add("--audio-quality", quality.ToString("D"));
        }

        public DownLoadParameters MetadataFromTitle(string format)
        {
            return Add("--metadata-from-title", format);
        }

        public DownLoadParameters VideoFormat(VideoFormat format)
        {
            return Add("--recode-video", format.ToString("G").ToLowerInvariant());
        }

        public DownLoadParameters IgnoreErrors()
        {
            return Add("--ignore-errors");
        }

        public DownLoadParameters SetPath(string path)
        {
            return Add("-P", $"\"{path}\"");
        }

        public DownLoadParameters GetJson()
        {
            return Add("-j");
        }

        public DownLoadParameters Update()
        {
            return Add("--update");
        }

        public DownLoadParameters Version()
        {
            return Add("--version");
        }

        internal DownLoadParameters Url(string url)
        {
            return Add($"\"{url}\"");
        }

        public DownLoadParameters FlatPlaylist()
        {
            return Add("--flat-playlist");
        }

        public DownLoadParameters LazyPlayList()
        {
            return Add("--lazy-playlist");
        }

        public DownLoadParameters NoCleanInfoJson()
        {
            return Add("--no-clean-info-json");
        }

        public DownLoadParameters ParseMetadata(string expression)
        {
            return Add("--parse-metadata", expression);
        }

        public DownLoadParameters ReplaceMetadata(string expression)
        {
            return Add("--replace-in-metadata", expression);
        }

        public DownLoadParameters Reset()
        {
            Clear();
            return this;
        }

        public List<string> GetParameters()
        {
            var parameters = new List<string>();
            foreach (var parameter in this)
            {
                parameters.Add(parameter.Key);

                if (!string.IsNullOrEmpty(parameter.Value))
                {
                    parameters.Add(parameter.Value);
                }
            }

            return parameters;
        }
    }
}