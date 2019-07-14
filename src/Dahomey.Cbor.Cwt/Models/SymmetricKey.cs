using Dahomey.Cbor.Attributes;
using Dahomey.Cbor.Cwt.Converters;
using System;

namespace Dahomey.Cbor.Cwt.Models
{
    [CborConverter(typeof(SymmetricKeyConverter))]
    public class SymmetricKey : KeyBase
    {
        public ReadOnlyMemory<byte> KeyValue { get; set; }
    }
}
