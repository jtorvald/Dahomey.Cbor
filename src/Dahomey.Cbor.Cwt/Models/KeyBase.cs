namespace Dahomey.Cbor.Cwt.Models
{
    public abstract class KeyBase
    {
        public KeyType KeyType { get; set; }
        public string KeyId { get; set; }
        public CwtAlgorithm Algorithm { get; set; }
    }
}
