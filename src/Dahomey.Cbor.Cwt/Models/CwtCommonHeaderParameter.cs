namespace Dahomey.Cbor.Cwt.Models
{
    /// <summary>
    /// Common COSE Headers Parameters
    /// </summary>
    /// <remarks>
    /// cf. https://tools.ietf.org/html/rfc8152#section-3.1
    /// </remarks>
    public enum CwtCommonHeaderParameter
    {
        /// <summary>
        /// Cryptographic algorithm to use
        /// </summary>
        Alg = 1,

        /// <summary>
        /// Critical headers to be understood
        /// </summary>
        Crit = 2,

        /// <summary>
        /// Content type of the payload
        /// </summary>
        ContentType = 3,

        /// <summary>
        /// Key identifier
        /// </summary>
        Kid = 4,

        /// <summary>
        /// Full initialization vector
        /// </summary>
        IV = 5,

        /// <summary>
        /// Partial initialization vector
        /// </summary>
        PartialIV = 6,

        /// <summary>
        /// CBOR-encoded signature structure
        /// </summary>
        CounterSignature = 7
    }
}
