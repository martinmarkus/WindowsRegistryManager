using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WindowsRegistryManager.Facades.Serializers
{
    internal class ByteArraySerializer : IByteArraySerializer
    {
        public byte[] Serialize<U>(U objectToSerialize)
        {
            MemoryStream memoryStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, objectToSerialize);
            byte[] result = null;

            try
            {
                result = memoryStream.ToArray();
            }
            catch (IOException e)
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
