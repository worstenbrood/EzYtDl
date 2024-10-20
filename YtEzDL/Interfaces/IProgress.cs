using static YtEzDL.Tools.YoutubeDownload;

namespace YtEzDL.Interfaces
{
    public interface IProgress
    {
        void Download(double progress);
        void FfMpeg(DownloadAction action, double progress);
    }
}
