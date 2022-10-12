using System.Xml;

namespace Kengic.Opns.Xml
{
    public class HmiTags :Xml
    {
        public HmiTags(string xml)
        {
            Load(xml);
            XmlNode attributeList = XmlElement?.FirstChild;
            LogicalAddress = attributeList?.SelectSingleNode(".//LogicalAddress")?.FirstChild;
            Name = attributeList?.SelectSingleNode(".//Name")?.FirstChild;
            XmlNode connection = XmlElement?.SelectSingleNode("//Connection");
            ConnectionName = connection?.FirstChild.FirstChild;
        }

        /// <summary>
        /// 获取AttributeList的LogicalAddress节点
        /// </summary>
        /// <returns><![CDATA[<LogicalAddress>%DB1003.DBW4</LogicalAddress>]]></returns>
        public XmlNode LogicalAddress;

        /// <summary>
        /// 获取AttributeList的Name节点
        /// </summary>
        /// <returns><![CDATA[<Name>AlarmWord1</Name>]]></returns>
        public XmlNode Name;

        /// <summary>
        /// 获取Connection的Name节点
        /// </summary>
        /// <returns><![CDATA[<Connection TargetID="@OpenLink">
        ///<Name>HMI_Connection_1</Name>
        ///</Connection>]]></returns>
        public XmlNode ConnectionName;

        /// <summary>
        /// 查询插入Tag的节点
        /// </summary>
        /// <returns><![CDATA[<ObjectList></ObjectList>]]></returns>
        public XmlNode InsertTag()
        {
            XmlNode xmlNode = XmlElement.SelectSingleNode("//ObjectList");
            return xmlNode;
        }
    }
}