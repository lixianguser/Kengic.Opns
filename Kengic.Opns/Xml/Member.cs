using System.Xml;

namespace Kengic.Opns.Xml
{
    public class Member : Xml
    {
        public Member(string xml)
        {
            Load(xml);
            XmlNode xmlNode = XmlElement?.SelectSingleNode("//Interface:Member", Xmlns);
            Name = xmlNode?.Attributes?[0];
        }

        /// <summary>
        /// 获取Member节点的Name属性
        /// </summary>
        /// <returns><![CDATA[<Member Name="DummyPlaceHolder" Datatype="DInt" Remanence="Retain" Accessibility="Public">
        /// </Member>]]></returns>
        public XmlAttribute Name;
    }
}