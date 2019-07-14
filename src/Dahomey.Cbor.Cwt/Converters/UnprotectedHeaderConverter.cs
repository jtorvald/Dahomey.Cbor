using Dahomey.Cbor.Cwt.Models;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using System;

namespace Dahomey.Cbor.Cwt.Converters
{
    public class UnprotectedHeaderConverter : CborConverterBase<UnprotectedHeader>
    {
        private readonly static ICborConverter<CwtCommonHeaderParameter> _headerParamConverter
             = CborConverter.Lookup<CwtCommonHeaderParameter>();

        private readonly static ICborConverter<ReadOnlyMemory<byte>> _memoryConverter
             = CborConverter.Lookup<ReadOnlyMemory<byte>>();

        public override UnprotectedHeader Read(ref CborReader reader)
        {
            reader.ReadBeginMap();
            int size = reader.ReadSize();

            UnprotectedHeader header = new UnprotectedHeader();

            for (int i = 0; i < size; i++)
            {
                ReadHeaderParameter(ref reader, header);
            }

            return header;
        }

        public override void Write(ref CborWriter writer, UnprotectedHeader value)
        {
            throw new NotImplementedException();
        }

        private void ReadHeaderParameter(ref CborReader reader, UnprotectedHeader header)
        {
            switch (_headerParamConverter.Read(ref reader))
            {
                case CwtCommonHeaderParameter.Kid:
                    header.Kid = _memoryConverter.Read(ref reader);
                    break;

                default:
                    reader.SkipDataItem();
                    break;
            }
        }
    }
}
