﻿using System;

namespace Dahomey.Cbor.Serialization.Converters.Providers
{
    public class ObjectConverterProvider : CborConverterProviderBase
    {
        public override ICborConverter GetConverter(Type type, SerializationRegistry registry)
        {
            if (type.IsClass || type.IsInterface)
            {
                return CreateGenericConverter(registry, typeof(ObjectConverter<>), type);
            }

            return null;
        }
    }
}
