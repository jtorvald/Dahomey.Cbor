using Dahomey.Cbor.Cwt.Models;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using System;
using System.Collections.Generic;

namespace Dahomey.Cbor.Cwt.Converters
{
    public class ProtectedHeaderConverter : CborConverterBase<ProtectedHeader>
    {
        private readonly static ICborConverter<CwtCommonHeaderParameter> _headerParamConverter
             = CborConverter.Lookup<CwtCommonHeaderParameter>();

        private readonly static ICborConverter<CwtAlgorithm> _algorithmConverter
             = CborConverter.Lookup<CwtAlgorithm>();

        private readonly static ICborConverter<List<CwtCommonHeaderParameter>> _critConverter
             = CborConverter.Lookup<List<CwtCommonHeaderParameter>>();

        public override ProtectedHeader Read(ref CborReader reader)
        {
            reader.ReadBeginMap();
            int size = reader.ReadSize();

            ProtectedHeader header = new ProtectedHeader();

            for (int i = 0; i < size; i++)
            {
                ReadHeaderParameter(ref reader, header);
            }

            return header;
        }

        public override void Write(ref CborWriter writer, ProtectedHeader value)
        {
            throw new NotImplementedException();
        }

        private void ReadHeaderParameter(ref CborReader reader, ProtectedHeader header)
        {
            switch (_headerParamConverter.Read(ref reader))
            {
                case CwtCommonHeaderParameter.Alg:
                    header.Alg = _algorithmConverter.Read(ref reader);
                    break;

                case CwtCommonHeaderParameter.Crit:
                    header.Crit = _critConverter.Read(ref reader);
                    break;

                default:
                    reader.SkipDataItem();
                    break;
            }
        }
    }
}
