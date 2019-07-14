using Dahomey.Cbor.Cwt.Models;
using Dahomey.Cbor.Cwt.Util;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using System.Text;

namespace Dahomey.Cbor.Cwt.Converters
{
    public abstract class KeyBaseConverter<T> : CborConverterBase<T>
        where T : KeyBase, new()
    {
        private readonly static ICborConverter<KeyMapLabel> _keyMapLabelConverter
             = CborConverter.Lookup<KeyMapLabel>();

        private readonly static ICborConverter<KeyType> _keyTypeConverter
             = CborConverter.Lookup<KeyType>();

        private readonly static ICborConverter<CwtAlgorithm> _algorithmConverter
             = CborConverter.Lookup<CwtAlgorithm>();

        public override T Read(ref CborReader reader)
        {
            reader.ReadBeginMap();

            T key = new T();

            int size = reader.ReadSize();

            for (int i = 0; i < size; i++)
            {
                KeyMapLabel label = _keyMapLabelConverter.Read(ref reader);
                ReadKeyProperty(ref reader, label, key);
            }

            return key;
        }

        public override void Write(ref CborWriter writer, T value)
        {
            throw new System.NotImplementedException();
        }

        protected virtual void ReadKeyProperty(ref CborReader reader, KeyMapLabel label, T key)
        {
            switch (label)
            {
                case KeyMapLabel.KeyType:
                    key.KeyType = _keyTypeConverter.Read(ref reader);
                    break;

                case KeyMapLabel.KeyId:
                    key.KeyId = Encoding.ASCII.GetString(reader.ReadByteString());
                    break;

                case KeyMapLabel.Algorithm:
                    key.Algorithm = _algorithmConverter.Read(ref reader);
                    break;

                default:
                    reader.SkipDataItem();
                    break;
            }
        }
    }
}
