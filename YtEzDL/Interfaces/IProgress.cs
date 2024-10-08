namespace YtEzDL.Interfaces
{
    public interface IProgress
    {
        void Download(double progress);
        void FfMpeg(double progress);
    }
}
