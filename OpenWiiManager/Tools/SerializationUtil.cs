using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OpenWiiManager.Tools
{
    public static class SerializationUtil
    {
        public static string SerializeToXml(this object? value)
        {
            return value.SerializeToXml(Encoding.UTF8);
        }

        public static string SerializeToXml(this object? value, Encoding encoding)
        {
            using (var memStream = new MemoryStream())
            {
                var serializer = new XmlSerializer(value?.GetType() ?? typeof(object));
                serializer.Serialize(memStream, value);

                return encoding.GetString(memStream.ToArray());
            }
        }
        public static XElement SerializeToXmlNode(this object? value)
        {
            return value.SerializeToXmlNode(Encoding.UTF8);
        }

        public static XElement SerializeToXmlNode(this object? value, Encoding encoding)
        {
            using (var memStream = new MemoryStream())
            {
                var serializer = new XmlSerializer(value?.GetType() ?? typeof(object));
                serializer.Serialize(memStream, value);

                var str = encoding.GetString(memStream.ToArray());
                var doc = XDocument.Parse(str);
                return doc.Root!;
            }
        }

        public static string SerializeToXml<T>(this T? value)
        {
            return value.SerializeToXml(Encoding.UTF8);
        }

        public static string SerializeToXml<T>(this T? value, Encoding encoding)
        {
            using (var memStream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(memStream, value);

                return encoding.GetString(memStream.ToArray());
            }
        }

        public static T? DeserializeFromXml<T>(string xml)
        {
            return DeserializeFromXml<T>(xml, Encoding.UTF8);
        }

        public static T? DeserializeFromXml<T>(string xml, Encoding encoding)
        {
            var bytes = encoding.GetBytes(xml);
            using (var memStream = new MemoryStream(bytes))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T?)serializer.Deserialize(memStream);
            }
        }

        public static object? DeserializeFromXml(Type t, string xml)
        {
            return DeserializeFromXml(t, xml, Encoding.UTF8);
        }

        public static object? DeserializeFromXml(Type t, string xml, Encoding encoding)
        {
            var bytes = encoding.GetBytes(xml);
            using (var memStream = new MemoryStream(bytes))
            {
                var serializer = new XmlSerializer(t);
                return serializer.Deserialize(memStream);
            }
        }

        public static object? DeserializeFromXmlNode(Type t, XElement xml)
        {
            return DeserializeFromXmlNode(t, xml, Encoding.UTF8);
        }

        public static object? DeserializeFromXmlNode(Type t, XElement xml, Encoding encoding)
        {
            var doc = new XDocument(xml);
            var bytes = encoding.GetBytes(doc.ToString());
            using (var memStream = new MemoryStream(bytes))
            {
                var serializer = new XmlSerializer(t);
                return serializer.Deserialize(memStream);
            }
        }
    }
}
