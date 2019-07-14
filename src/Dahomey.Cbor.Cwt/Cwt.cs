using Dahomey.Cbor.Cwt.Handlers;
using Dahomey.Cbor.Cwt.Models;
using Dahomey.Cbor.Serialization;
using System;

namespace Dahomey.Cbor.Cwt
{
    public static class Cwt
    {
        public static bool ValidateToken(ReadOnlySpan<byte> token, ReadOnlySpan<byte> key)
        {
            CborReader reader = new CborReader(token);

            if (!reader.TryReadSemanticTag(out ulong semanticTag) || semanticTag != CwtTags.CWT)
            {
                return false;
            }

            if (!reader.TryReadSemanticTag(out semanticTag))
            {
                return false;
            }

            switch (semanticTag)
            {
                case CwtTags.COSE_Sign:
                case CwtTags.COSE_Sign1:
                    return ValidateSigningObject(ref reader);

                case CwtTags.COSE_Mac:
                    return ValidateMacWithRecipients(ref reader);

                case CwtTags.COSE_Mac0:
                    return ValidateMac0Object(ref reader, key);

                case CwtTags.COSE_Encrypt:
                case CwtTags.COSE_Encrypt0:
                    return ValidateEncryptionObject(ref reader);

                default:
                    return false;
            }
        }

        private static bool ValidateSigningObject(ref CborReader reader)
        {
            throw new NotImplementedException();
        }

        private static bool ValidateMac0Object(ref CborReader reader, ReadOnlySpan<byte> keyBuffer)
        {
            SymmetricKey key = CoseKeySerializer.DeserializeSymmetricKey(keyBuffer);
            CwtPayload payload = CwtMac0ObjectHandler.ValideToken(ref reader, key);
            return payload != null;
        }

        private static bool ValidateMacWithRecipients(ref CborReader reader)
        {
            return true;
        }
        
        private static bool ValidateEncryptionObject(ref CborReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
