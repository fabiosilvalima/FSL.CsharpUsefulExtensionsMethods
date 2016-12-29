using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace FSL.CsharpUsefulExtensionsMethods
{
    public static class FSLSerializationExtension
    {        
        public static string ToJson<T>(this T obj)
        {
            if (obj != null)
            {
                return JsonConvert.SerializeObject(obj);
            }

            return null;
        }
        
        public static T FromJson<T>(this string json)
        {
            if (string.IsNullOrEmpty(json)) return default(T);

            return (T)JsonConvert.DeserializeObject(json);
        }
        
        public static T FromXml<T>(this string xml)
        {
            if (string.IsNullOrEmpty(xml)) return default(T);

            T result;
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            using (StringReader str = new StringReader(xml))
            {
                result = (T)xmlSer.Deserialize(str);
            }

            return result;
        }
        
        public static string ToXml<T>(this T obj)
        {
            if (obj == null) return null;

            string result = "";
            XmlSerializer xmlSer = new XmlSerializer(obj.GetType());
            using (MemoryStream m = new MemoryStream())
            {
                xmlSer.Serialize(m, obj);
                result = Encoding.UTF8.GetString(m.GetBuffer()).Replace("\0", "");
            }

            return result;
        }
    }
}
