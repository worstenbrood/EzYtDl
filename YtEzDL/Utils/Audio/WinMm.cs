using System;
using System.Runtime.InteropServices;
using System.Text;

namespace YtEzDL.Utils.Audio
{
    public enum WaveFormatTags : ushort
    {
        Pcm = 0x0001,
        IeeeFloat = 0x0003,
        Alaw = 0x0006,
        MuLaw = 0x0007,
        MpegLayer3 = 0x0055,
        Extensible = 0xFFFE
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WaveFormatEx
    {
        public WaveFormatTags FormatTag;
        public ushort Channels;
        public uint SamplesPerSec;
        public uint AvgBytesPerSec;
        public ushort BlockAlign;
        public ushort BitsPerSample;
        public ushort Size;
    }

    [Flags]
    public enum WaveOpenFlags
    {
        Null = 0,
        Function = 0x30000,
        Event = 0x50000,
        Window = 0x10000,
        CallbackThread = 0x20000,
    }

    public enum WaveMessage
    {
        InOpen = 0x3BE,
        InClose = 0x3BF,
        InData = 0x3C0,
        OutClose = 0x3BC,
        OutDone = 0x3BD,
        OutOpen = 0x3BB
    }

    [Flags]
    public enum WaveHeaderFlags
    {
        Prepared = 0x00000002,
        Done = 0x00000001,
        BeginLoop = 0x00000004,
        EndLoop = 0x00000008,
        InQueue = 0x00000010,
    }

    public class WaveHeader
    {
        public IntPtr DataBuffer;
        public int BufferLength;
        public int BytesRecorded;
        public IntPtr UserData;
        public WaveHeaderFlags Flags;
        public int Loops;
        public IntPtr Next;
        public IntPtr Reserved;
    }

    public delegate void WaveCallback(IntPtr hWaveOut, WaveMessage message, IntPtr dwInstance, WaveHeader wavHdr, IntPtr dwReserved);

    public enum MmResult
    {
        NoError = 0,
        UnspecifiedError = 1,
        BadDeviceId = 2,
        NotEnabled = 3,
        AlreadyAllocated = 4,
        InvalidHandle = 5,
        NoDriver = 6,
        MemoryAllocationError = 7,
        NotSupported = 8,
        BadErrorNumber = 9,
        InvalidFlag = 10,
        InvalidParameter = 11,
        HandleBusy = 12,
        InvalidAlias = 13,
        BadRegistryDatabase = 14,
        RegistryKeyNotFound = 15,
        RegistryReadError = 16,
        RegistryWriteError = 17,
        RegistryDeleteError = 18,
        RegistryValueNotFound = 19,
        NoDriverCallback = 20,
        MoreData = 21,
        WaveBadFormat = 32,
        WaveStillPlaying = 33,
        WaveHeaderUnprepared = 34,
        WaveSync = 35,
        AcmNotPossible = 512,
        AcmBusy = 513,
        AcmHeaderUnprepared = 514,
        AcmCancelled = 515,
        MixerInvalidLine = 1024,
        MixerInvalidControl = 1025,
        MixerInvalidValue = 1026,
    }
    
    public class WinMm
    {
        [DllImport(nameof(WinMm))]
        public static extern int waveOutGetNumDevs();
        
        [DllImport(nameof(WinMm))]
        public static extern MmResult waveOutPrepareHeader(IntPtr hWaveOut, WaveHeader lpWaveOutHdr, int uSize);
        
        [DllImport(nameof(WinMm))]
        public static extern MmResult waveOutUnprepareHeader(IntPtr hWaveOut, WaveHeader lpWaveOutHdr, int uSize);
        
        [DllImport(nameof(WinMm))]
        public static extern MmResult waveOutWrite(IntPtr hWaveOut, WaveHeader lpWaveOutHdr, int uSize);

        [DllImport(nameof(WinMm))]
        public static extern MmResult waveOutOpen(out IntPtr hWaveOut, IntPtr uDeviceId, WaveFormatEx lpFormat, WaveCallback dwCallback, IntPtr dwInstance, WaveOpenFlags dwFlags);

        [DllImport(nameof(WinMm))]
        public static extern MmResult waveOutReset(IntPtr hWaveOut);

        [DllImport(nameof(WinMm))]
        public static extern MmResult waveOutClose(IntPtr hWaveOut);

        [DllImport(nameof(WinMm))]
        public static extern MmResult waveOutPause(IntPtr hWaveOut);

        [DllImport(nameof(WinMm))]
        public static extern MmResult waveOutRestart(IntPtr hWaveOut);

        [DllImport(nameof(WinMm))]
        public static extern MmResult waveOutSetVolume(IntPtr hWaveOut, int dwVolume);

        [DllImport(nameof(WinMm))]
        public static extern MmResult waveOutGetVolume(IntPtr hWaveOut, out int dwVolume);

        [DllImport(nameof(WinMm), CharSet = CharSet.Auto)]
        public static extern MmResult waveOutGetErrorText(MmResult mmrError, StringBuilder pszText, uint cchText);
    }
}
