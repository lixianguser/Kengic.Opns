using System.Xml;

namespace Kengic.Opns.Xml
{
    public class Sheet : Xml
    {
        private const string ss = "urn:schemas-microsoft-com:office:spreadsheet";
        private const string o = "urn:schemas-microsoft-com:office:office";
        private const string x = "urn:schemas-microsoft-com:office:excel";

        /// <summary>
        /// 获取IO DEF工作表节点
        /// </summary>
        /// <returns><![CDATA[<Worksheet ss:Name="IO DEF"></Worksheet>]]></returns>
        public XmlNode IoDef_Worksheet;

        /// <summary>
        /// 获取IO DEF工作表的Row节点列表
        /// </summary>
        /// <returns><![CDATA[<Row></Row>]]></returns>
        public XmlNodeList IoDef_Row;

        /// <summary>
        /// 获取IO DEF工作表的Location节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">CC2</Data>]]></returns>
        public XmlNode IoDef_Location;

        /// <summary>
        /// 获取IO DEF工作表的IoDevice节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">SIE.## S7-1515-2 PN ##.PLC.PN2</Data>]]></returns>
        public XmlNode IoDevice;

        /// <summary>
        /// 获取IO DEF工作表的DT节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">A1</Data>]]></returns>
        public XmlNode DT;

        /// <summary>
        /// 获取IO DEF工作表的InputAddress节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">-</Data>]]></returns>
        public XmlNode InputAddress;

        /// <summary>
        /// 获取IO DEF工作表的OutputAddress节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">-</Data>]]></returns>
        public XmlNode OutputAddress;

        /// <summary>
        /// 获取IO MAP工作表节点
        /// </summary>
        /// <returns><![CDATA[<Worksheet ss:Name="IO MAP"></Worksheet>]]></returns>
        public XmlNode IoMap_Worksheet;

        /// <summary>
        /// 获取IO MAP工作表的Row节点列表
        /// </summary>
        /// <returns><![CDATA[<Row></Row>]]></returns>
        public XmlNodeList IoMap_Row;

        /// <summary>
        /// 获取IO MAP工作表的Symbol节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">CC2I_StartSystem</Data>]]></returns>
        public XmlNode Symbol;

        /// <summary>
        /// 获取IO MAP工作表的Comment节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">Cabinet Start Pushbutton</Data>]]></returns>
        public XmlNode Comment;

        /// <summary>
        /// 获取IO MAP工作表的Address节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">I0.0</Data>]]></returns>
        public XmlNode Address;

        /// <summary>
        /// 获取IO MAP工作表的DataType节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">BOOL</Data>]]></returns>
        public XmlNode DataType;

        /// <summary>
        /// 获取IO MAP工作表的Location节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">CC2</Data>]]></returns>
        public XmlNode IoMap_Location;

        /// <summary>
        /// 获取FBList工作表节点
        /// </summary>
        /// <returns><![CDATA[<Worksheet ss:Name="IO DEF"></Worksheet>]]></returns>
        public XmlNode FBList_Worksheet;

        /// <summary>
        /// 获取FBList工作表的Row节点列表
        /// </summary>
        /// <returns><![CDATA[<Row></Row>]]></returns>
        public XmlNodeList FBList_Row;

        /// <summary>
        /// 获取FBList工作表的DBName节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">CC2</Data>]]></returns>
        public XmlNode DBName;

        /// <summary>
        /// 获取FBList工作表的FBName节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">CC00_(HMI)</Data>]]></returns>
        public XmlNode FBName;

        /// <summary>
        /// 获取FBList工作表的InstanceDBNumber节点
        /// </summary>
        /// <returns><![CDATA[]]></returns>
        public XmlNode InstanceDBNumber;

        /// <summary>
        /// 获取FBList工作表的Ports节点
        /// </summary>
        /// <returns><![CDATA[]]></returns>
        public XmlNode Ports;

        /// <summary>
        /// 获取EZ工作表节点
        /// </summary>
        /// <returns><![CDATA[<Worksheet ss:Name="EZ"></Worksheet>]]></returns>
        public XmlNode EZ_Worksheet;

        /// <summary>
        /// 获取EZ工作表的Row节点列表
        /// </summary>
        /// <returns><![CDATA[<Row></Row>]]></returns>
        public XmlNodeList EZ_Row;

        /// <summary>
        /// 获取EZ工作表的Distribution节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">CC2</Data>]]></returns>
        public XmlNode Distribution;

        /// <summary>
        /// 获取EZ工作表的Designation节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">EZ01</Data>]]></returns>
        public XmlNode Designation;

        /// <summary>
        /// 获取EZ工作表的ConveyorNumber节点
        /// </summary>
        /// <returns><![CDATA[<Data ss:Type="String">P3093</Data>]]></returns>
        public XmlNode ConveyorNumber;

        public Sheet(string xml)
        {
            Load(xml);
            AddNamespace();
            IoMap_Worksheet =
                XmlElement?.SelectSingleNode("//ss:Worksheet[@ss:Name='IO MAP']", Xmlns);
            IoMap_Row = IoMap_Worksheet?.SelectNodes(".//ss:Row", Xmlns);
            IoDef_Worksheet =
                XmlElement?.SelectSingleNode("//ss:Worksheet[@ss:Name='IO DEF']", Xmlns);
            IoDef_Row = IoDef_Worksheet?.SelectNodes(".//ss:Row", Xmlns);
            FBList_Worksheet =
                XmlElement?.SelectSingleNode("//ss:Worksheet[@ss:Name='FB LIST']", Xmlns);
            FBList_Row = FBList_Worksheet?.SelectNodes(".//ss:Row", Xmlns);
            EZ_Worksheet =
                XmlElement?.SelectSingleNode("//ss:Worksheet[@ss:Name='EZ']", Xmlns);
            EZ_Row = EZ_Worksheet?.SelectNodes(".//ss:Row", Xmlns);
        }

        /// <summary>
        /// 获取IO MAP的节点
        /// </summary>
        /// <param name="rowNumber">轮询Row节点</param>
        public void GetIoMap(int rowNumber)
        {
            XmlNodeList data = GetData(IoMap_Row, rowNumber);
            Symbol = data?[0].FirstChild;
            Comment = data?[1].FirstChild;
            Address = data?[2].FirstChild;
            DataType = data?[3].FirstChild;
            IoMap_Location = data?[4].FirstChild;
        }

        /// <summary>
        /// 获取IO DEF的节点
        /// </summary>
        /// <param name="rowNumber">轮询Row节点</param>
        public void GetIoDef(int rowNumber)
        {
            XmlNodeList data = GetData(IoDef_Row, rowNumber);
            IoDef_Location = data?[0].FirstChild;
            IoDevice = data?[1].FirstChild;
            DT = data?[2].FirstChild;
            InputAddress = data?[3].FirstChild;
            OutputAddress = data?[4].FirstChild;
        }

        /// <summary>
        /// 获取FB LIST的节点
        /// </summary>
        /// <param name="rowNumber">轮询Row节点</param>
        public void GetFBList(int rowNumber)
        {
            XmlNodeList data = GetData(FBList_Row, rowNumber);
            DBName = data?[0].FirstChild;
            FBName = data?[1].FirstChild;
            InstanceDBNumber = data?[2]?.FirstChild;
            Ports = data?[3]?.FirstChild;
        }

        /// <summary>
        /// 获取EZ的节点
        /// </summary>
        /// <param name="rowNumber">轮询Row节点</param>
        public void GetEZ(int rowNumber)
        {
            XmlNodeList data = GetData(EZ_Row, rowNumber);
            Distribution = data?[0].FirstChild;
            Designation = data?[1].FirstChild;
        }

        /// <summary>
        /// 获取EZ输送机编号节点
        /// </summary>
        /// <param name="rowNumber">轮询Row节点</param>
        /// <param name="dataNumber">轮询DataList节点</param>
        /// <returns><![CDATA[<Data ss:Type="String">P3093</Data>]]></returns>
        public void GetConveyorNumber(int rowNumber, int dataNumber)
        {
            XmlNodeList data = GetData(EZ_Row, rowNumber);
            ConveyorNumber = data?[dataNumber].FirstChild;
        }

        /// <summary>
        /// 添加命名空间
        /// </summary>
        private void AddNamespace()
        {
            Xmlns.AddNamespace("ss", ss);
            Xmlns.AddNamespace("o", o);
            Xmlns.AddNamespace("x", x);
        }

        /// <summary>
        /// 获取Data节点
        /// </summary>
        /// <param name="rowList">Row节点列表</param>
        /// <param name="rowNumber">轮询Row节点列表</param>
        /// <returns><![CDATA[<Data></Data>]]></returns>
        private XmlNodeList GetData(XmlNodeList rowList, int rowNumber)
        {
            XmlNodeList xmlNodeList = rowList[rowNumber].SelectNodes(".//ss:Data", Xmlns);
            return xmlNodeList;
        }
    }
}