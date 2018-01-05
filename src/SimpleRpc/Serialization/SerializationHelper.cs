﻿using System;
using System.Collections.Generic;
using SimpleRpc.Serialization.MsgPack;
using SimpleRpc.Serialization.Wire;

namespace SimpleRpc.Serialization
{
    public static class SerializationHelper
    {
        private static readonly IDictionary<string, IMessageSerializer> _contentTypeSerializer = new Dictionary<string, IMessageSerializer>(StringComparer.OrdinalIgnoreCase);
        private static readonly IDictionary<string, IMessageSerializer> _nameSerializer = new Dictionary<string, IMessageSerializer>(StringComparer.OrdinalIgnoreCase);

        static SerializationHelper()
        {
            Add(new WireMessageSerializer());
            Add(new MsgPackSerializer());
        }

        public static void Add(IMessageSerializer serializer)
        {
            _nameSerializer.Add(serializer.Name, serializer);
            _contentTypeSerializer.Add(serializer.ContentType, serializer);
        }

        public static IMessageSerializer GetByName(string name)
        {
            return _nameSerializer[name];
        }

        public static IMessageSerializer GetByContentType(string contentType)
        {
            return _contentTypeSerializer[contentType];
        }
    }
}
