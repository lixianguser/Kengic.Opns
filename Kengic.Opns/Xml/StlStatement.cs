using System.Xml;

namespace Kengic.Opns.Xml
{
    public class StlStatement : Xml
    {
        public StlStatement(string xml)
        {
            Load(xml);
            XmlNodeList xmlNodeList = XmlElement?.SelectNodes("//StatementList:Component[@Name]", Xmlns);
            InstanceDB = xmlNodeList?[0].Attributes?[0];
            Parameter = xmlNodeList?[1].Attributes?[0];
            Type = xmlNodeList?[2].Attributes?[0];
            Static = xmlNodeList?[3].Attributes?[0];
        }

        /// <summary>
        /// 获取StlStatement节点的实例DB属性
        /// </summary>
        /// <returns><![CDATA[<Component Name="EZ01" />]]></returns>
        public XmlAttribute InstanceDB;

        /// <summary>
        /// 获取StlStatement节点的监控的参数属性
        /// </summary>
        /// <returns><![CDATA[<Component Name="Y_DV_ZoneTripped" />]]></returns>
        public XmlAttribute Parameter;

        /// <summary>
        /// 获取StlStatement节点的监控的类型属性
        /// </summary>
        /// <returns><![CDATA[<Component Name="SCADA_BOOL_ALRM" />]]></returns>
        public XmlAttribute Type;

        /// <summary>
        /// 获取StlStatement节点的静态变量属性
        /// </summary>
        /// <returns><![CDATA[<Component Name="EZ01_Y_DV_ZoneTripped" />]]></returns>
        public XmlAttribute Static;
    }
}