using System;
using System.Runtime.InteropServices;

namespace YtEzDL.Utils.Audio
{
    public class WaveOutBuffer : IDisposable
    {
        private readonly WaveHeader _header;
        private readonly int _bufferSize; // allocated bytes, may not be the same as bytes read
        private readonly byte[] _buffer;
        private readonly object _waveOutLock;
        private GCHandle _bufferHandle;
        private IntPtr _waveOut;
        private GCHandle _headerHandle; // we need to pin the header structure
        private GCHandle _thisHandle; // for the user callback

        /// <summary>
        /// creates a new wavebuffer
        /// </summary>
        /// <param name="hWaveOut">WaveOut device to write to</param>
        /// <param name="bufferSize">Buffer size in bytes</param>
        /// <param name="waveOutLock">Lock to protect WaveOut API's from being called on >1 thread</param>
        public WaveOutBuffer(IntPtr hWaveOut, int bufferSize, object waveOutLock)
        {
            _bufferSize = bufferSize;
            _buffer = new byte[bufferSize];
            _bufferHandle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);
            
            _waveOut = hWaveOut; 
            _waveOutLock = waveOutLock;

            _header = new WaveHeader();
            _headerHandle = GCHandle.Alloc(_header, GCHandleType.Pinned);
            _header.DataBuffer = _bufferHandle.AddrOfPinnedObject();
            _header.BufferLength = bufferSize;
            _header.Loops = 1;
            
            _thisHandle = GCHandle.Alloc(this);
            _header.UserData = (IntPtr)_thisHandle;
            lock (_waveOutLock)
            {
                WinMmException.Try(WinMm.waveOutPrepareHeader(hWaveOut, _header, Marshal.SizeOf(_header)));
            }
        }

        #region Dispose Pattern

        /// <summary>
        /// Finalizer for this wave buffer
        /// </summary>
        ~WaveOutBuffer()
        {
            Dispose(false);
            System.Diagnostics.Debug.Assert(true, "WaveBuffer was not disposed");
        }

        /// <summary>
        /// Releases resources held by this WaveBuffer
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases resources held by this WaveBuffer
        /// </summary>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free unmanaged resources
            if (_headerHandle.IsAllocated)
                _headerHandle.Free();
            if (_bufferHandle.IsAllocated)
                _bufferHandle.Free();
            if (_thisHandle.IsAllocated)
                _thisHandle.Free();
            if (_waveOut != IntPtr.Zero)
            {
                lock (_waveOutLock)
                {
                    WinMm.waveOutUnprepareHeader(_waveOut, _header, Marshal.SizeOf(_header));
                }
                _waveOut = IntPtr.Zero;
            }
        }

        #endregion

        /// this is called by the WAVE callback and should be used to refill the buffer
        public bool Write(byte[] buffer, int length)
        {
            if (length == 0)
            {
                return false;
            }

            lock (_waveOutLock)
            {
                Marshal.Copy(buffer, 0, _header.DataBuffer, _buffer.Length);
            }
            
            for (var n = length; n < buffer.Length; n++)
            {
                buffer[n] = 0;
            }

            WriteToWaveOut();
            return true;
        }
    
        /// <summary>
        /// Whether the header's in queue flag is set
        /// </summary>
        public bool InQueue
        {
            get
            {
                lock (_waveOutLock)
                {
                    return (_header.Flags & WaveHeaderFlags.InQueue) == WaveHeaderFlags.InQueue;
                }
            }
        }

        /// <summary>
        /// The buffer size in bytes
        /// </summary>
        public int BufferSize => _bufferSize;

        private void WriteToWaveOut()
        {
            MmResult result;

            lock (_waveOutLock)
            {
                result = WinMm.waveOutWrite(_waveOut, _header, Marshal.SizeOf(_header));
            }
            if (result != MmResult.NoError)
            {
                throw new WinMmException(result);
            }

            GC.KeepAlive(this);
        }
    }
}
