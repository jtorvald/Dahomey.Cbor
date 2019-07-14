using Dahomey.Cbor.Attributes;
using Dahomey.Cbor.Cwt.Converters;
using System;

namespace Dahomey.Cbor.Cwt.Models
{
    [CborConverter(typeof(CwtPayloadConverter))]
    public class CwtPayload
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime NotBefore { get; set; }
        public DateTime IssuedAt { get; set; }
        public ReadOnlyMemory<byte> CwtId { get; set; }
    }
}
