using Dahomey.Cbor.Cwt.Models;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using System;

namespace Dahomey.Cbor.Cwt.Converters
{
    public class SymmetricKeyConverter : KeyBaseConverter<SymmetricKey>
    {
        private readonly static ICborConverter<ReadOnlyMemory<byte>> _memoryConverter
             = CborConverter.Lookup<ReadOnlyMemory<byte>>();

        protected override void ReadKeyProperty(ref CborReader reader, KeyMapLabel label, SymmetricKey key)
        {
            switch(label)
            {
                // key value 'k'
                case (KeyMapLabel)(-1):
                    key.KeyValue = _memoryConverter.Read(ref reader);
                    break;

                default:
                    base.ReadKeyProperty(ref reader, label, key);
                    break;
            }
        }
    }
}
