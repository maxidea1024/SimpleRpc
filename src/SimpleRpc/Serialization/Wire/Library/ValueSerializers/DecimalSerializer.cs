﻿#region copyright
// -----------------------------------------------------------------------
//  <copyright file="DecimalSerializer.cs" company="Akka.NET Team">
//      Copyright (C) 2015-2016 AsynkronIT <https://github.com/AsynkronIT>
//      Copyright (C) 2016-2016 Akka.NET Team <https://github.com/akkadotnet>
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System;
using System.IO;

namespace SimpleRpc.Serialization.Wire.Library.ValueSerializers
{
    internal sealed class DecimalSerializer : ValueSerializer
    {
        public const byte Manifest = 14;
        public static readonly DecimalSerializer Instance = new DecimalSerializer();

        public override void WriteManifest(Stream stream, SerializerSession session)
        {
            stream.WriteByte(Manifest);
        }

        public override void WriteValue(Stream stream, object value, SerializerSession session)
        {
            var bytes = session.GetBuffer(Int32Serializer.Size);

            var data = decimal.GetBits((decimal) value);
            Int32Serializer.WriteValueImpl(stream, data[0], bytes);
            Int32Serializer.WriteValueImpl(stream, data[1], bytes);
            Int32Serializer.WriteValueImpl(stream, data[2], bytes);
            Int32Serializer.WriteValueImpl(stream, data[3], bytes);
        }

        public override object ReadValue(Stream stream, DeserializerSession session)
        {
            var bytes = session.GetBuffer(Int32Serializer.Size);

            var parts = new[]
            {
                Int32Serializer.ReadValueImpl(stream, bytes),
                Int32Serializer.ReadValueImpl(stream, bytes),
                Int32Serializer.ReadValueImpl(stream, bytes),
                Int32Serializer.ReadValueImpl(stream, bytes)
            };
            var sign = (parts[3] & 0x80000000) != 0;

            var scale = (byte) ((parts[3] >> 16) & 0x7F);
            var newValue = new decimal(parts[0], parts[1], parts[2], sign, scale);
            return newValue;
        }

        public override Type GetElementType()
        {
            return typeof(decimal);
        }
    }
}