using Dahomey.Cbor.Cwt.Models;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using System;

namespace Dahomey.Cbor.Cwt.Converters
{
    public class CwtPayloadConverter : CborConverterBase<CwtPayload>
    {
        private static ICborConverter<CwtClaimKey> _claimKeyConverter
            = CborConverter.Lookup<CwtClaimKey>();

        private static ICborConverter<string> _stringConverter
            = CborConverter.Lookup<string>();

        private static ICborConverter<DateTime> _dateTimeConverter
            = CborConverter.Lookup<DateTime>();

        private readonly static ICborConverter<ReadOnlyMemory<byte>> _memoryConverter
             = CborConverter.Lookup<ReadOnlyMemory<byte>>();

        public override CwtPayload Read(ref CborReader reader)
        {
            reader.ReadBeginMap();
            int size = reader.ReadSize();

            CwtPayload payload = new CwtPayload();

            for (int i = 0; i < size; i++)
            {
                ReadClaim(ref reader, payload);
            }

            return payload;
        }

        public override void Write(ref CborWriter writer, CwtPayload value)
        {
            throw new NotImplementedException();
        }

        private void ReadClaim(ref CborReader reader, CwtPayload payload)
        {
            switch (_claimKeyConverter.Read(ref reader))
            {
                case CwtClaimKey.Issuer:
                    payload.Issuer = _stringConverter.Read(ref reader);
                    break;
                case CwtClaimKey.Subject:
                    payload.Subject = _stringConverter.Read(ref reader);
                    break;

                case CwtClaimKey.Audience:
                    payload.Audience = _stringConverter.Read(ref reader);
                    break;

                case CwtClaimKey.ExpirationTime:
                    payload.ExpirationTime = _dateTimeConverter.Read(ref reader);
                    break;

                case CwtClaimKey.NotBefore:
                    payload.NotBefore = _dateTimeConverter.Read(ref reader);
                    break;

                case CwtClaimKey.IssuedAt:
                    payload.IssuedAt = _dateTimeConverter.Read(ref reader);
                    break;

                case CwtClaimKey.CwtId:
                    payload.CwtId = _memoryConverter.Read(ref reader);
                    break;
            }
        }
    }
}
