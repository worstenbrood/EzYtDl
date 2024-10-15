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

        public DownLoadParameters RemoveCache()
        {
            this["--rm-cache-dir"] = string.Empty;
            return this;
        }

        public DownLoadParameters ExtractAudio()
        {
            this["-x"] = string.Empty;
            return this;
        }

        public DownLoadParameters AddMetadata()
        {
            this["--add-metadata"] = string.Empty;
            return this;
        }

        public DownLoadParameters EmbedThumbnail()
        {
            this["--embed-thumbnail"] = string.Empty;
            return this;
        }

        public DownLoadParameters AudioFormat(AudioFormat format)
        {
            this["--audio-format"] = format.ToString("G").ToLowerInvariant();
            return this;
        }

        public DownLoadParameters AudioQuality(AudioQuality quality)
        {
            this["--audio-quality"] = quality.ToString("D");
            return this;
        }

        public DownLoadParameters MetadataFromTitle(string format)
        {
            this["--metadata-from-title"] = format;
            return this;
        }

        public DownLoadParameters VideoFormat(VideoFormat format)
        {
            this["--recode-video"] = format.ToString("G").ToLowerInvariant();
            return this;
        }

        public DownLoadParameters IgnoreErrors()
        {
            this["--ignore-errors"] = string.Empty;
            return this;
        }

        public DownLoadParameters SetPath(string path)
        {
            this["-P"] = $"\"{path}\"";
            return this;
        }

        public DownLoadParameters GetJson()
        {
            this["-j"] = string.Empty;
            return this;
        }

        public DownLoadParameters Update()
        {
            this["--update"] = string.Empty;
            return this;
        }

        public DownLoadParameters Version()
        {
            this["--version"] = string.Empty;
            return this;
        }

        internal DownLoadParameters Url(string url)
        {
            this[$"\"{url}\""] = string.Empty;
            return this;
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