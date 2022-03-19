using System;
using System.Runtime.InteropServices;

namespace ElysiumPlayer.AAudio
{
    public class PinnedData: IDisposable
    {
        private GCHandle _handle; 
        internal PinnedData(object data)
        {
            _handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        }

        public static PinnedData Pin<T>(T data) => new PinnedData(data);

        public IntPtr Addr => _handle.AddrOfPinnedObject();

        ~PinnedData()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_handle.IsAllocated)
            {
                _handle.Free();
            }
        }
    }
}