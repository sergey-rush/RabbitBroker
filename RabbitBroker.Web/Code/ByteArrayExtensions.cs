using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RabbitBroker.Web.Code
{
    public static class ByteArrayExtensions
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static T FromByteArray<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return null;
            }
            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                memoryStream.Write(byteArray, 0, byteArray.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var obj = (T)binaryFormatter.Deserialize(memoryStream);
                return obj;
            }
        }
    }
}