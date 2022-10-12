using System.Xml;

namespace Kengic.Opns.Xml
{
    public class AlarmScadaMap : Xml
    {
        public XmlNodeList Section;
        public XmlNodeList StlStatement;

        public AlarmScadaMap(string xml)
        {
            Load(xml);
            Section = XmlElement.SelectNodes("//Interface:Section[@Name='Static']", Xmlns);
            StlStatement = XmlElement.SelectNodes("//StatementList:StatementList", Xmlns);
        }

        /// <summary>
        /// 查询插入DB块静态变量的节点
        /// </summary>
        /// <param name="type">报警变量的类型</param>
        /// <returns><![CDATA[<Section Name="Static">
        /// </Section>]]></returns>
        public XmlNode InsertDB(string type)
        {
            XmlNode xmlNode = null;
            switch (type)
            {
                case "SCADA_BOOL_ALRM":
                    xmlNode = Section?[0];
                    break;
                case "SCADA_BOOL_STAT":
                    xmlNode = Section?[1];
                    break;
            }

            return xmlNode;
        }

        /// <summary>
        /// 查询插入Stl语句的节点
        /// </summary>
        /// <param name="type"></param>
        /// <returns><![CDATA[<StatementList xmlns="http://www.siemens.com/automation/Openness/SW/NetworkSource/StatementList/v4">
        /// </StatementList>]]></returns>
        public XmlNode InsertStl(string type)
        {
            XmlNode xmlNode = null;
            switch (type)
            {
                case "SCADA_BOOL_ALRM":
                    xmlNode = StlStatement?[0];
                    break;
                case "SCADA_BOOL_STAT":
                    xmlNode = StlStatement?[1];
                    break;
            }

            return xmlNode;
        }

        /// <summary>
        /// 重设UId
        /// </summary>
        public void ResetUId()
        {
            XmlNodeList xmlNodeList = XmlElement.SelectNodes("//StatementList:StlStatement[@UId]", Xmlns);
            int uId = 21;
            if (xmlNodeList != null)
            {
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    if (xmlNode.Attributes != null)
                    {
                        xmlNode.Attributes[0].Value = uId.ToString();
                        uId++;
                    }
                }
            }
        }
    }
}