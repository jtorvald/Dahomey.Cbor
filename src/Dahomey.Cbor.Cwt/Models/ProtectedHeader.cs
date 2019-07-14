using Dahomey.Cbor.Attributes;
using Dahomey.Cbor.Cwt.Converters;
using System.Collections.Generic;

namespace Dahomey.Cbor.Cwt.Models
{
    [CborConverter(typeof(ProtectedHeaderConverter))]
    public class ProtectedHeader
    {
        public CwtAlgorithm Alg { get; set; }
        public List<CwtCommonHeaderParameter> Crit { get; set; }
    }
}
