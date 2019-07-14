using System;
using System.Text;

namespace Dahomey.Cbor.Cwt.Util
{
    internal static class EncodingExtensions
    {
        public static unsafe string GetString(this Encoding encoding, ReadOnlySpan<byte> bytes)
        {
            fixed (byte* bytesPtr = &bytes.GetPinnableReference())
            {
                return bytesPtr == null ? string.Empty : encoding.GetString(bytesPtr, bytes.Length);
            }
        }
    }
}
