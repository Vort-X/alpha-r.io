using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace r.io.shared
{
#pragma warning disable SYSLIB0011
    public class Serializer<T>
    {
        private readonly IFormatter formatter;

        public Serializer()
        {
            formatter = new BinaryFormatter();
        }

        public T Deserialize(byte[] data)
        {
            MemoryStream ms = new();
            ms.Write(data, 0, data.Length);
            ms.Position = 0;
            T t = (T)formatter.Deserialize(ms);
            ms.Close();
            return t;
        }

        public byte[] Serialize(T obj)
        {
            MemoryStream ms = new();
            formatter.Serialize(ms, obj);
            byte[] data = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(data, 0, (int)ms.Length);
            ms.Close();
            return data;
        }
    }
#pragma warning restore SYSLIB0011
}
