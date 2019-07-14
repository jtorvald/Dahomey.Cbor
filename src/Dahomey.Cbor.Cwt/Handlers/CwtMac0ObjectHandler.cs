using Dahomey.Cbor.Cwt.Models;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using Dahomey.Cbor.Util;
using System;
using System.Security.Cryptography;

namespace Dahomey.Cbor.Cwt.Handlers
{
    public static class CwtMac0ObjectHandler
    {
        private readonly static ICborConverter<ProtectedHeader> _protectedHeaderConverter
            = CborConverter.Lookup<ProtectedHeader>();

        private readonly static ICborConverter<UnprotectedHeader> _unprotectedHeaderConverter
            = CborConverter.Lookup<UnprotectedHeader>();

        private readonly static ICborConverter<CwtPayload> _payloadConverter
            = CborConverter.Lookup<CwtPayload>();

        public static CwtPayload ValideToken(ref CborReader reader, SymmetricKey key)
        {
            if (reader.GetCurrentDataItemType() != CborDataItemType.Array)
            {
                return null;
            }

            reader.ReadBeginArray();

            if (reader.ReadSize() != 4)
            {
                return null;
            }

            // protected header
            ReadOnlySpan<byte> wrappedProtectedHeader = reader.ReadByteString();
            CborReader protectedHeaderReader = new CborReader(wrappedProtectedHeader);
            ProtectedHeader protectedHeader = _protectedHeaderConverter.Read(ref protectedHeaderReader);

            // unprotected header
            UnprotectedHeader unprotectedHeader = _unprotectedHeaderConverter.Read(ref reader);

            // payload
            ReadOnlySpan<byte> wrappedPayload = reader.ReadByteString();
            CborReader payloadReader = new CborReader(wrappedPayload);
            CwtPayload payload = _payloadConverter.Read(ref payloadReader);

            // tag
            ReadOnlySpan<byte> tag = reader.ReadByteString();

            using (ByteBufferWriter macStructure = CreateMacStructure(
                wrappedProtectedHeader, wrappedPayload))
            {
                ReadOnlySpan<byte> signature = ComputeSignature(
                    macStructure.WrittenSpan, key.KeyValue.Span, key.Algorithm);

                if (!tag.SequenceEqual(signature))
                {
                    return null;
                }
            }

            return payload;
        }

        private static ByteBufferWriter CreateMacStructure(
            ReadOnlySpan<byte> wrappedProtectedHeader,
            ReadOnlySpan<byte> wrappedPayload)
        {
            ByteBufferWriter buffer = new ByteBufferWriter();
            CborWriter writer = new CborWriter(buffer);

            writer.WriteBeginArray(4);
            writer.WriteString("MAC0");
            writer.WriteByteString(wrappedProtectedHeader);
            writer.WriteByteString(new byte[0]);
            writer.WriteByteString(wrappedPayload);

            return buffer;
        }

        private static ReadOnlySpan<byte> ComputeSignature(
            ReadOnlySpan<byte> macStructure,
            ReadOnlySpan<byte> key,
            CwtAlgorithm algorithm)
        {
            using (HMAC hmac = CreateHmacAlgorithm(algorithm))
            {
                hmac.Key = key.ToArray();
                byte[] hash = hmac.ComputeHash(macStructure.ToArray());
                Span<byte> signature = new Span<byte>(hash, 0, GetSignatureByteLength(algorithm));
                return signature;
            }
        }

        private static HMAC CreateHmacAlgorithm(CwtAlgorithm algorithm)
        {
            switch(algorithm)
            {
                case CwtAlgorithm.HMAC_256_256:
                case CwtAlgorithm.HMAC_256_64:
                    return new HMACSHA256();

                case CwtAlgorithm.HMAC_384_384:
                    return new HMACSHA384();

                case CwtAlgorithm.HMAC_512_512:
                    return new HMACSHA512();

                default:
                    throw new NotSupportedException();
            }
        }

        private static int GetSignatureByteLength(CwtAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case CwtAlgorithm.HMAC_256_256:
                    return 32;

                case CwtAlgorithm.HMAC_256_64:
                    return 8;

                case CwtAlgorithm.HMAC_384_384:
                    return 48;

                case CwtAlgorithm.HMAC_512_512:
                    return 64;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
