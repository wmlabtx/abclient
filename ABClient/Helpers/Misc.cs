using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ABClient.Helpers
{
    public static class Misc
    {
        public static object DeepClone(object obj)
        {
            object objResult;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }

            return objResult;
        }
    }
}
