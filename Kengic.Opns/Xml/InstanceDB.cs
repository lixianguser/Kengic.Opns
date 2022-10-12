using System.Xml;

namespace Kengic.Opns.Xml
{
    public class InstanceDB : Xml
    {
        /// <summary>
        /// 获取InstanceOfName的节点
        /// </summary>
        /// <returns><![CDATA[<InstanceOfName>CC00_(HMI)</InstanceOfName>]]></returns>
        public XmlNode InstanceOfName;

        /// <summary>
        /// 获取Name的节点
        /// </summary>
        /// <returns><![CDATA[<Name>CC1</Name>]]></returns>
        public XmlNode Name;

        /// <summary>
        /// 获取Number的节点
        /// </summary>
        /// <returns><![CDATA[<Number>20</Number>]]></returns>
        public XmlNode Number;

        /// <summary>
        /// 获取AutoNumber的节点
        /// </summary>
        /// <returns><![CDATA[<AutoNumber>false</AutoNumber>]]></returns>
        public XmlNode AutoNumber;

        /// <summary>
        /// 获取AttributeList的节点
        /// </summary>
        /// <returns><![CDATA[<AttributeList></AttributeList>]]></returns>
        public XmlNode AttributeList;

        public InstanceDB(string xml)
        {
            Load(xml);
            InstanceOfName = XmlElement.SelectSingleNode("//InstanceOfName")?.FirstChild;
            Name = XmlElement.SelectSingleNode("//Name")?.FirstChild;
            Number = XmlElement.SelectSingleNode("//Number");
            AutoNumber = XmlElement.SelectSingleNode("//AutoNumber");
            AttributeList = XmlElement.SelectSingleNode("//AttributeList");
        }

        /// <summary>
        /// 将实例背景DB块设置为自动编号
        /// </summary>
        private void SetAutoNumber()
        {
            if (AutoNumber != null)
                AutoNumber.FirstChild.Value = "true";
            if (AttributeList != null)
                if (Number != null)
                    AttributeList.RemoveChild(Number);
        }

        /// <summary>
        /// 创建一个实例背景DB块
        /// </summary>
        /// <param name="instanceOfName">FB块名称</param>
        /// <param name="name">实例名称</param>
        /// <param name="number">编号</param>
        public void Create(string instanceOfName, string name, string number)
        {
            if (InstanceOfName != null)
                InstanceOfName.Value = instanceOfName;
            if (Name != null)
                Name.Value = name;
            if (!string.IsNullOrEmpty(number))
            {
                if (Number != null)
                    Number.FirstChild.Value = number;
            }
            else
            {
                SetAutoNumber();
            }
        }

        /// <summary>
        /// 设置实例背景DB块变量具有保持性
        /// </summary>
        public void SetRemanence()
        {
            XmlNodeList remanenceCompilations =
                XmlElement?.SelectNodes("//Interface:Member[@Remanence='NonRetain']", Xmlns);
            if (remanenceCompilations != null)
            {
                foreach (XmlNode remanence in remanenceCompilations)
                {
                    if (remanence.Attributes != null)
                    {
                        foreach (XmlAttribute attribute in remanence.Attributes)
                        {
                            if (attribute.Name == "Remanence")
                            {
                                attribute.Value = "Retain";
                            }
                        }
                    }
                }
            }
        }
    }
}