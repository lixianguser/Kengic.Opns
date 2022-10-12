using System;
using System.Collections.Generic;
using System.Linq;
using Kengic.Opns.Database;

namespace Kengic.Opns.Xml
{
    public class PlcTagTables : Xml
    {
        public PlcTagTables(IReadOnlyCollection<IOMap> IoMap,string version)
        {
            Load(XmlString(IoMap,version));
        }

        private static string XmlString(IReadOnlyCollection<IOMap> IoMap,string version)
        {
            string ret = "";
            int id = 0;
            int items = 0;

            ret += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;//XML头
            ret += "<Document>" + Environment.NewLine;//XML文档
            ret += "<Engineering version=\"" + version + "\"/> ";//博图版本号

            var list = IoMap.GroupBy(x => x.location).Where(x => x.Any()).ToList();

            foreach (var Name in list.Select(item => list[items++].Key))
            {
                ret += "<SW.Tags.PlcTagTable ID=\"" + id++ + "\">" + Environment.NewLine;//plc变量表元素
                ret += "<AttributeList>" + Environment.NewLine;
                ret += "<Name>" + Name + "</Name>" + Environment.NewLine;//plc变量表名称
                ret += "</AttributeList>" + Environment.NewLine;
                ret += "<ObjectList>" + Environment.NewLine;

                var groups = IoMap.Where(p => p.location == Name);

                foreach (IOMap ioMap in groups)
                {
                    ret += "<SW.Tags.PlcTag ID=\"" + id++ + "\" CompositionName=\"Tags\">" + Environment.NewLine;//plc变量元素
                    ret += "<AttributeList>" + Environment.NewLine;
                    ret += "<DataTypeName>Bool</DataTypeName>" + Environment.NewLine;//plc变量数据类型
                    ret += "<ExternalAccessible>true</ExternalAccessible>" + Environment.NewLine;//可外部访问
                    ret += "<ExternalVisible>true</ExternalVisible>" + Environment.NewLine;//
                    ret += "<ExternalWritable>true</ExternalWritable>" + Environment.NewLine;//
                    ret += "<LogicalAddress>%" + ioMap.address + "</LogicalAddress>" + Environment.NewLine;//plc变量地址
                    ret += "<Name>" + ioMap.symbol + "</Name>" + Environment.NewLine;//plc变量名称
                    ret += "</AttributeList>" + Environment.NewLine;
                    ret += "<ObjectList>" + Environment.NewLine;
                    ret += "<MultilingualText ID=\"" + id++ + "\" CompositionName=\"Comment\">" + Environment.NewLine;
                    ret += "<ObjectList>" + Environment.NewLine;
                    ret += "<MultilingualTextItem ID=\"" + id++ + "\" CompositionName=\"Items\">" + Environment.NewLine;
                    ret += "<AttributeList>" + Environment.NewLine;
                    ret += "<Culture>en-US</Culture>" + Environment.NewLine;
                    ret += "<Text>" + ioMap.comment + "</Text>" + Environment.NewLine;//plc变量备注
                    ret += "</AttributeList>" + Environment.NewLine;
                    ret += "</MultilingualTextItem>" + Environment.NewLine;
                    ret += "</ObjectList>" + Environment.NewLine;
                    ret += "</MultilingualText>" + Environment.NewLine;
                    ret += "</ObjectList>" + Environment.NewLine;
                    ret += "</SW.Tags.PlcTag>" + Environment.NewLine;
                }

                ret += "</ObjectList>" + Environment.NewLine;
                ret += "</SW.Tags.PlcTagTable>" + Environment.NewLine;
            }

            ret += "</Document>" + Environment.NewLine;

            return ret;
        }
    }
}