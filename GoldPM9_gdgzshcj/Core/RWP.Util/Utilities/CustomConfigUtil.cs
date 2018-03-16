/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2014/11/28
*           175417739@qq.com                   
********************************************************************/

using System;
using System.Xml;

namespace JQ.Util
{
    /// <summary>
    /// 自定义Config助手，主要用于缓存参数
    /// </summary>
    public static class CustomConfigUtil
    {
        private static string _customConfigPath;
        private const string nodeName = "appSettings";

        /// <summary>
        /// 根据配置节key，获取相应值value
        /// </summary>
        /// <param name="keyName">健</param>
        /// <returns>值</returns>
        public static string getConfigValue(string keyName)
        {
            inIt();
            return getConfigValue(_customConfigPath, keyName);
        }
        /// <summary>
        /// 根据配置节key，获取相应值value
        /// </summary>
        /// <param name="configPath">路径</param>
        /// <param name="keyName">健值</param>
        /// <returns></returns>
        public static string getConfigValue(string configPath, string keyName)
        {
            configPath = getConfigPath(configPath);
            XmlDocument xDoc = getXmlDocument(configPath);
            XmlNode xNode;
            XmlElement xElem = null;
            xNode = xDoc.SelectSingleNode("//" + nodeName);
            if (xNode != null)
            {
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + keyName + "\"]");
            }
            if (xElem != null)
                return xElem.GetAttribute("value");
            else
                return string.Empty;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private static void inIt()
        {
            if (string.IsNullOrEmpty(_customConfigPath))
            {
                //自定义config路径，从默认config 中获取
                _customConfigPath = ConfigUtil.GetConfigValue("customConfig");
            }
        }

        /// <summary>
        /// 更新值
        /// </summary>
        /// <param name="keyName">健</param>
        /// <param name="keyValue">值</param>
        /// <returns></returns>
        public static bool updateConfig(string keyName, string keyValue)
        {
            inIt();
            return updateConfig(_customConfigPath, keyName, keyValue);
        }

        /// <summary>
        /// 更新值
        /// </summary>
        /// <param name="configPath">路径</param>
        /// <param name="keyName">健</param>
        /// <param name="keyValue">值</param>
        /// <returns></returns>
        public static bool updateConfig(string configPath, string keyName, string keyValue)
        {
            configPath = getConfigPath(configPath);
            XmlDocument xDoc = getXmlDocument(configPath);

            //XmlNodeList
            xDoc.SelectNodes("//appSettings");

            XmlNode xNode;
            XmlElement xElem = null;
            xNode = xDoc.SelectSingleNode("//" + nodeName);
            if (xNode == null)
            {
                return false;
            }
            xElem = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + keyName + "\"]");
            if (xElem == null)
            {
                xElem = xDoc.CreateElement("add");
                xElem.SetAttribute("key", keyName);
                xElem.SetAttribute("value", keyValue);
                xNode.AppendChild(xElem);
            }
            else
            {
                xElem.SetAttribute("value", keyValue);
            }
            xDoc.Save(configPath);
            return true;
        }

        /// <summary>
        /// 获取XML
        /// </summary>
        /// <param name="configPath">路径</param>
        /// <returns></returns>
        public static XmlDocument getXmlDocument(string configPath)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(configPath);
            return xDoc;
        }

        /// <summary>
        /// 文件夹路径，加上文件名
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        private static string getConfigPath(string configPath)
        {
            if (_customConfigPath == configPath)
            {
                configPath = AppDomain.CurrentDomain.BaseDirectory + "\\" + _customConfigPath;
            }
            else if (StringUtil.endsWithIgnoreCase(configPath, "\\"))
            {
                if (string.IsNullOrEmpty(_customConfigPath))
                {
                    inIt();
                }
                string path = configPath + _customConfigPath;
                if (IOUtil.fileExists(path))
                {
                    configPath = path;
                }
                else
                {
                    string[] paths = IOUtil.getFilesForDirectory(configPath, "*.Config");
                    if (paths.isNotEmpty())
                    {
                        configPath = paths[0];
                    }
                }
            }
            return configPath;
        }
    }
}
