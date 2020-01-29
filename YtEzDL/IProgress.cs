namespace YtEzDL
{
    public interface IProgress
    {
        void Download(double progress);
        void FfMpeg(double progress);
    }
}
