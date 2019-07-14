namespace Dahomey.Cbor.Cwt.Models
{
    public enum CwtAlgorithm
    {
        Undefined = 0,

        ES256 = -7,
        ES384 = -35,
        ES512 = -36,

        EdDSA = -8,

        HMAC_256_64 = 4,
        HMAC_256_256 = 5,
        HMAC_384_384 = 6,
        HMAC_512_512 = 7,

        AES_MAC_128_64 = 14,
        AES_MAC_256_64 = 15,
        AES_MAC_128_128 = 25,
        AES_MAC_256_128 = 26,

        A128GCM = 1,
        A192GCM = 2,
        A246GCM = 3,

        AES_CCM_16_64_128 = 10,
        AES_CCM_16_64_256 = 11,
        AES_CCM_64_64_128 = 12,
        AES_CCM_64_64_256 = 13,
        AES_CCM_16_128_128 = 30,
        AES_CCM_16_128_256 = 31,
        AES_CCM_64_128_128 = 32,
        AES_CCM_64_128_256 = 33,

        CHACHA20_POLY1305 = 24,

        SALT = -20,

        PARTYU_IDENTITY = -21,
        PARTYU_NONCE = -22,
        PARTYU_OTHER = -23,
        PARTYV_IDENTITY = -24,
        PARTYV_NONCE = -25,
        PARTYV_OTHER = -26,

        DIRECT = -6,

        DIRECT_HKDF_SHA_256 = -10,
        DIRECT_HKDF_SHA_512 = -11,
        DIRECT_HKDF_AES_128 = -12,
        DIRECT_HKDF_AES_256 = -13,

        A128KW = -3,
        A192KW = -4,
        A256KW = -5,
    }
}
