using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsRegistryManager.Facades.Serializers
{
    internal class ByteArraySerializer : IByteArraySerializer
    {
        //public byte[] Serialize<U>(U objectToSerialize)
        //{
        //    if (objectToSerialize == null)
        //    {
        //        throw new ArgumentNullException();
        //    }

        //    XmlSerializer serializer = new XmlSerializer(typeof(U));

        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream))
        //        {
        //            serializer.Serialize(xmlWriter, objectToSerialize);
        //            return memoryStream.ToArray();
        //        }
        //    }
        //}

        //public U Deserialize<U>(byte[] byteArray)
        //{
        //    if (byteArray == null || byteArray.Length == 0)
        //    {
        //        throw new InvalidOperationException();
        //    }

        //    XmlSerializer serializer = new XmlSerializer(typeof(U));

        //    using (MemoryStream memoryStream = new MemoryStream(byteArray))
        //    {
        //        using (XmlReader xmlReader = XmlReader.Create(memoryStream))
        //        {
        //            return (U)serializer.Deserialize(xmlReader);
        //        }
        //    }
        //}

        public byte[] Serialize<U>(U objectToSerialize)
        {
            MemoryStream memoryStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            byte[] result = null;

            try
            {
                formatter.Serialize(memoryStream, objectToSerialize);
                result = memoryStream.ToArray();
            }
            catch (Exception e) when (e is IOException || e is SerializationException)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public U Deserialize<U>(byte[] byteArray)
        {
            MemoryStream memoryStream = new MemoryStream(byteArray);
            IFormatter formatter = new BinaryFormatter();
            U result = default(U);

            try
            {
                result = (U)formatter.Deserialize(memoryStream);
            }
            catch (Exception e) when (e is ArgumentException)
            {
                Console.WriteLine(e.Message);
            }
            catch (SerializationException e)
            {
                throw e;
            }

            return result;
        }
    }
}
