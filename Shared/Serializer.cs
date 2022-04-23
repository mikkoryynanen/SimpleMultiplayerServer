using System.Runtime.Serialization.Formatters.Binary;

namespace Shared;

public class Serializer
{
    // Convert an object to a byte array
    public static byte[] ObjectToByteArray(Object obj)
    {
        if(obj == null)
            return null;

        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, obj);

        return ms.ToArray();
    }

    // Convert a byte array to an Object
    public static T ByteArrayToObject<T>(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        T obj = (T) binForm.Deserialize(memStream);

        return obj;
    }
}