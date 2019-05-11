using System.IO;
using UnityEngine;

namespace Scripts.Save
{
    public static class JsonSerializator
    {
        public static void SerializeClass(string json, string fileName, string path)
        {
            File.WriteAllText(path + "/" + fileName, json);
        }

        public static void SerializeClass<TClass>(TClass @class, string fileName, string path) where TClass : class
        {
            File.WriteAllText(path + "/" + fileName, JsonUtility.ToJson(@class));
        }

        public static TClass ReadSerialize<TClass>(string fileName, string path) where TClass : class
        {
            if (!File.Exists(path + "/" + fileName))
            {
                return null;
            }

            using (StreamReader streamReader = new StreamReader(path + "/" + fileName))
            {
                return JsonUtility.FromJson<TClass>(streamReader.ReadLine());
            }
        }
    }
}