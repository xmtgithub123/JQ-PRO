using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace Common
{
    public class XmlConvert
    {
        /// <summary>
        /// 将object对象序列化成XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ObjectToXML<T>(T t)
        {
            Encoding encoding = Encoding.UTF8;
            XmlSerializer ser = new XmlSerializer(t.GetType());
            using (MemoryStream mem = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(mem, encoding))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    ser.Serialize(writer, t, ns);
                    return encoding.GetString(mem.ToArray()).Trim();
                }
            }
        }

        public static string HashTableToXml(Hashtable ht)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration dec = xdoc.CreateXmlDeclaration("1.0", Encoding.UTF8.ToString(), "yes");

            XmlElement rootXml = xdoc.CreateElement("root");
            foreach (DictionaryEntry de in ht)
            {
                XmlElement ele = xdoc.CreateElement(de.Key.ToString());
                ele.InnerText = de.Value == null ? "" : de.Value.ToString();
                rootXml.AppendChild(ele);
            }
            xdoc.AppendChild(rootXml);
            return xdoc.InnerXml;
        }


        /// <summary>
        /// 将XML反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T XMLToObject<T>(string source)
        {
            Encoding encoding = Encoding.UTF8;
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream(encoding.GetBytes(source)))
            {
                return (T)mySerializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// 二进制方式序列化对象
        /// </summary>
        /// <param name="testUser"></param>
        public static string Serialize<T>(T obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            return ms.ToString();
        }

        /// <summary>
        /// 二进制方式反序列化对象
        /// </summary>
        /// <returns></returns>
        public static T DeSerialize<T>(string str) where T : class
        {
            MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(str));
            BinaryFormatter formatter = new BinaryFormatter();
            T t = formatter.Deserialize(ms) as T;
            return t;
        }

        /// <summary>
        /// 获取某列的值
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string GetXmlValue(string xml, string Name)
        {
            Hashtable hash = XmlToHash(xml);
            if (hash.ContainsKey(Name))
                return hash[Name].ToString();
            return string.Empty;
        }

        /// <summary>
        /// 将xml转化成hash
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static Hashtable XmlToHash(string xml)
        {
            Hashtable hash = new Hashtable();
            if (xml == "")
            {
                return hash;
            }
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration dec = xdoc.CreateXmlDeclaration("1.0", Encoding.UTF8.ToString(), "yes");
            xdoc.InnerXml = xml;
            XmlNode xnode = xdoc.DocumentElement;//获取根节点
            string value = string.Empty;
            foreach (XmlElement element in xnode.ChildNodes)//对子节点进行遍历
            {
                hash.Add(element.Name, element.InnerText);
            }
            return hash;
        }
    }

    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region IXmlSerializable 成员

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool isEmpty = reader.IsEmptyElement;
            reader.Read();
            if (isEmpty)
                return;
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                this.Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();

        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }

        }

        #endregion
    }

    public class SerializableHashtable : System.Collections.Hashtable, IXmlSerializable
    {
        #region IXmlSerializable 成员

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }


        private XmlSerializer serializer = new XmlSerializer(typeof(object));



        public void ReadXml(System.Xml.XmlReader reader)
        {
            bool isEmpty = reader.IsEmptyElement;
            reader.Read();
            if (isEmpty)
                return;
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                object key = serializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                object value = serializer.Deserialize(reader);
                reader.ReadEndElement();
                this[key] = value;
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();

        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (object key in this.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                serializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                object value = this[key];
                serializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }

        }

        #endregion
    }
}
