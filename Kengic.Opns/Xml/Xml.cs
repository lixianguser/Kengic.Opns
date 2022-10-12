using System.IO;
using System.Xml;

namespace Kengic.Opns.Xml
{
    public class Xml
    {
        
        private const string Interface = "http://www.siemens.com/automation/Openness/SW/Interface/v4";

        private const string StatementList =
            "http://www.siemens.com/automation/Openness/SW/NetworkSource/StatementList/v4";

        private const string FlgNet = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4";

        public XmlDocument XmlDocument;
        public XmlElement XmlElement;
        public XmlNamespaceManager Xmlns;

        /// <summary>
        /// 加载Xml文档
        /// </summary>
        /// <param name="xml">Xml文件名或者Xml字符串</param>
        protected void Load(string xml)
        {
            //加载Xml文档
            XmlDocument = new XmlDocument();
            if (xml == null) return;
            var extension = xml.Substring(xml.Length - 4, 4);
            if (extension == ".xml") XmlDocument.Load(xml); //从文件中加载Xml文档
            else XmlDocument.LoadXml(xml); //从字符串加载Xml文档
            //获取Xml根节点
            XmlElement = XmlDocument.DocumentElement;
            //设置Xml命名空间
            Xmlns = new XmlNamespaceManager(XmlDocument.NameTable);
            Xmlns.AddNamespace("Interface", Interface);
            Xmlns.AddNamespace("StatementList", StatementList);
            Xmlns.AddNamespace("FlgNet",FlgNet);
        }

        /// <summary>
        /// 重设ID编号
        /// </summary>
        public void ResetID()
        {
            XmlNodeList xmlNodeList = XmlElement.SelectNodes("//@ID");
            int id = 0;
            if (xmlNodeList != null)
            {
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    xmlNode.Value = id.ToString();
                    id++;
                }
            }
        }
    }
}