using Dahomey.Cbor.Attributes;
using Dahomey.Cbor.Cwt.Converters;
using System;

namespace Dahomey.Cbor.Cwt.Models
{
    [CborConverter(typeof(UnprotectedHeaderConverter))]
    public class UnprotectedHeader
    {
        public ReadOnlyMemory<byte> Kid { get; set; }
    }
}
