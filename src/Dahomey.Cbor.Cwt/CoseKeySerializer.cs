using Dahomey.Cbor.Cwt.Models;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using System;

namespace Dahomey.Cbor.Cwt
{
    public static class CoseKeySerializer
    {
        private readonly static ICborConverter<SymmetricKey> _symmetricKeyConverter
            = CborConverter.Lookup<SymmetricKey>();

        public static SymmetricKey DeserializeSymmetricKey(ReadOnlySpan<byte> keyBuffer)
        {
            CborReader reader = new CborReader(keyBuffer);
            return _symmetricKeyConverter.Read(ref reader);
        }
    }
}
