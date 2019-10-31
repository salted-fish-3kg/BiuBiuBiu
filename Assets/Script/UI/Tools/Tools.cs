using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Knight.Tools
{
    public static class Tools
    {
        public static Transform FindChildByName(this Transform transform, string name)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform tf = transform.GetChild(i);
                if (tf.name == name) return tf;
                tf = FindChildByName(tf, name);
                if (tf != null && tf.name == name) return tf;
            }
            return null;
        }
        public static T LoadPrefab<T>(string address) where T : Object
        {
            T obj = Resources.Load<T>(address);
            obj = Object.Instantiate(obj);
            return obj;
        }
        public static void FileRead(string path,ref string data)
        {
            if (!File.Exists(path)) Debug.Log("NotExists"+path);
            StringBuilder str = new StringBuilder();
            using (FileStream reader = new FileStream(path, FileMode.Open,FileAccess.Read))
            {
                byte[] buffer = new byte[1024];
                int length = 1;
                while ((length = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    str.Append(Encoding.Default.GetString(buffer, 0, length));
                }
            }
            data = str.ToString();
        }
        public static void FileWrite(string path, string data,FileMode fileMode)
        {
            if (!File.Exists(path))
            {
                Debug.Log("NotExists" + path);
                File.Create(path);
            }
            using (FileStream write = new FileStream(path, fileMode, FileAccess.Write))
            {
                byte[] buffer = Encoding.Default.GetBytes(data);
                write.Write(buffer, 0, buffer.Length);
            }

        }
    }
}
