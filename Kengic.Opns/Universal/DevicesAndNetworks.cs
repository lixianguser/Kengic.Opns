using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Kengic.Opns.Command;
using Kengic.Opns.Database;
using Kengic.Opns.UI;
using Kengic.Opns.Utility;
using Kengic.Opns.Xml;
using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;

namespace Kengic.Opns.Universal
{
    public class DevicesAndNetworks
    {
        private static TiaPortal _tiaPortal => AddInProvider._tiaPortal;
        private static Project _project => _tiaPortal.Projects.First();

        /// <summary>
        /// 分布式IO的耦合模块名称
        /// </summary>
        private static string _interfaceModule { get; set; }

        /// <summary>
        /// 扫码器设备数量
        /// </summary>
        private static int _scannerCount { get; set; }

        /// <summary>
        /// 称重设备数量
        /// </summary>
        private static int _scalesCount { get; set; }

        /// <summary>
        /// PLC的设备项合集
        /// </summary>
        private static DeviceItemComposition _deviceItemComposition { get; set; }

        public static void Import_Execution(MenuSelectionProvider<DeviceItem> menuSelectionProvider)
        {
            PlcTarget plcTarget = new PlcTarget(menuSelectionProvider.GetSelection());
            using (ExclusiveAccess exclusiveAccess = _tiaPortal.ExclusiveAccess("导入设备组态中..."))
            {
                using (Transaction transaction =
                       exclusiveAccess.Transaction(plcTarget.Project, "导入设备组态"))
                {
                    //获取配置文件数据
                    Sheet sheet = new Sheet(plcTarget.ConfigFilePath);
                    Connection connection = new Connection();
                    connection.Open();
                    var IoDefs = new List<IODef>();
                    for (int i = 1; i < sheet.IoDef_Row.Count; i++)
                    {
                        sheet.GetIoDef(i);
                        IODef ioDef = new IODef
                        {
                            Device = sheet.IoDevice.Value,
                            InputStartAddress = sheet.InputAddress.Value,
                            OutputStartAddress = sheet.OutputAddress.Value,
                            DeviceName = string.Concat(sheet.IoDef_Location.Value, "-", sheet.DT.Value)
                        };
                        DataReader.IO_DEF.Get(ioDef, connection.SqlConnection);
                        IoDefs.Add(ioDef);
                    }

                    //创建设备
                    _deviceItemComposition = plcTarget.DeviceItem.DeviceItems;
                    foreach (IODef ioDef in IoDefs)
                    {
                        exclusiveAccess.Text = "导入设备组态:" + ioDef.DeviceName;
                        Create(ioDef);
                    }

                    if (transaction.CanCommit)
                    {
                        transaction.CommitOnDispose();
                    }
                }
            }
        }

        public static void Hardware_Execution(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
            //新建一个DataBaseForm框体
            DataBaseForm dataBaseForm = new DataBaseForm();
            //给DataGridForm 对话框标题
            dataBaseForm.Text = "支持的硬件目录";
            DataBaseForm.DataMember = "def";
            //连接数据库
            Connection connection = new Connection();
            connection.Open();
            //获取def表的数据填充到DataSet
            DataSet dataSet = DataReader.IO_DEF.Get(DataBaseForm.DataMember,connection.SqlConnection);
            //将填充好的DataSet写入到DataGridForm
            DataBaseForm.DataSet = dataSet;
            //打开对话框框体
            dataBaseForm.ShowDialog(Command.UI.Top());
        }

        public static void ExportNetworkInfo_Execution(MenuSelectionProvider<Project> menuSelectionProvider)
        {
            StreamWriter streamWriter = null;
            List<string> attributeInfo = null;
            var attributesNodes = new List<AttributesNode>();
            PlcTarget plcTarget = new PlcTarget(menuSelectionProvider.GetSelection());
            using (ExclusiveAccess exclusiveAccess = _tiaPortal.ExclusiveAccess("导出网络信息中..."))
            {
                using (Transaction transaction =
                       exclusiveAccess.Transaction(plcTarget.Project, "导出网络信息"))
                {
                    foreach (Subnet subnet in plcTarget.Project.Subnets)
                    {
                        foreach (Node node in subnet.Nodes)
                        {
                            //获取节点所有可访问的元素信息
                            attributeInfo = new List<string>(new[]
                            {
                                "Address", "Name", "NodeId", "NodeType", "PnDeviceName", "SubnetMask",
                                "UseIpProtocol", "UseIsoProtocol", "UseRouter"
                            });
                            //获取元素信息的值
                            AttributesNode attributes = new AttributesNode
                            {
                                Attribute = node.GetAttributes(attributeInfo)
                            };

                            attributesNodes.Add(attributes);
                        }

                        //保存信息为.csv文件

                        streamWriter = new StreamWriter(plcTarget.CSVFilePth);
                        //设置列标题
                        StringBuilder strColumn = new StringBuilder();
                        if (attributeInfo != null)
                        {
                            foreach (var info in attributeInfo)
                            {
                                strColumn.Append(info);
                                strColumn.Append(",");
                            }
                        }

                        strColumn.Remove(strColumn.Length - 1, 1);
                        streamWriter.WriteLine(strColumn);
                        //设置行数据
                        StringBuilder strValue = new StringBuilder();
                        foreach (AttributesNode attributes in attributesNodes)
                        {
                            strValue.Remove(0, strValue.Length);
                            for (int i = 0; i < attributes.Attribute.Count; i++)
                            {
                                strValue.Append(attributes.Attribute[i]);
                                strValue.Append(",");
                            }

                            streamWriter.WriteLine(strValue);
                        }

                        streamWriter?.Dispose();
                    }

                    if (transaction.CanCommit)
                    {
                        transaction.CommitOnDispose();
                    }
                }
            }
        }

        private class AttributesNode
        {
            public IList<object> Attribute { get; set; }
        }

        /// <summary>
        /// 创建设备组态
        /// </summary>
        /// <param name="ioDef"></param>
        private static void Create(IODef ioDef)
        {
            switch (ioDef.Device)
            {
                case "SIE.## ET200MP Basic ##.IOM.RACK.PN2":
                case "SIE.## ET200MP Standard ##.IOM.RACK.PN2":
                    SIE_ET200MP(ioDef);
                    break;
                case "SIE.## ET200SP IM155-6 PN High Feature ##.IO.PN2":
                case "SIE.## ET200SP IM155-6 Basic ##.IO.PN2":
                case "SIE.## ET200SP IM155-6 Standard ##.IO.PN2":
                    SIE_ET200SP(ioDef);
                    break;
                // case "SIE.## ET200SP IM155-6 DP High Feature ##.IO.DP"://无
                //     break;
                case "SIE.## ET200SP 8In ##.IO.SLV":
                case "SIE.## ET200SP 16In ##.IO.SLV":
                case "SIE.## ET200SP 8Out ##.IO.SLV":
                case "SIE.## ET200SP 16Out ##.IO.SLV":
                case "SIE.## ET200SP 4/8 F-DI ##.IOF.SLV":
                case "SIE.## ET200SP 4 F-DO ##.IOF.SLV":
                case "SIE.## ET200SP 1 F-RQ ##.IOF.SLV":
                    DistributedIoSlv(ioDef);
                    break;
                case "WEI.## UR20-FBC-PN-IRT ##.IO.PN2":
                case "WEI.## UR20-FBC-PN-ECO ##.IO.PN2":
                case "WEI.## UR20-BASIC-FBC-PN ##.IO.PN2":
                    WeidmullerIo(ioDef);
                    break;
                case "WEI.## UR20 8In ##.IO.SLV":
                case "WEI.## UR20 16In ##.IO.SLV":
                case "WEI.## UR20 8Out ##.IO.SLV":
                case "WEI.## UR20 16Out ##.IO.SLV":
                case "WEI.## UR20-BASIC-8In ##.IO.SLV":
                case "WEI.## UR20-BASIC-16In ##.IO.SLV":
                case "WEI.## UR20-BASIC-8Out ##.IO.SLV":
                case "WEI.## UR20-BASIC-16Out ##.IO.SLV":
                    WeidmullerIoSlv(ioDef);
                    break;
                case "SIE.## SM521-16In ##.IO.RACK":
                case "SIE.## SM521-32In ##.IO.RACK":
                case "SIE.## SM522-16Out ##.IO.RACK":
                case "SIE.## SM522-32Out ##.IO.RACK":
                    ControllerIo(ioDef);
                    break;
                case "COGNEX.## DM260 ##.BS.PN":
                    _scannerCount++;
                    Cognex(ioDef);
                    break;
                case "## Generic Ethernet Weigh Scale ##.WS.PN2":
                    _scalesCount++;
                    WeighScale(ioDef);
                    break;
                case "GSEE.## GXPI20-DIO16S ##.IO.PN2":
                    GseeIo(ioDef);
                    break;
            }
        }

        #region DeviceItem property

        /// <summary>
        /// 连接设备子网到"PN/IE_1"，并连接到IO系统
        /// </summary>
        /// <param name="deviceName">设备名称</param>
        /// <param name="subNode">子网节点</param>
        private static void Subnet(string deviceName, int subNode)
        {
            //获取设备子网节点
            DeviceItem deviceItem = _project.Devices.Find(deviceName).DeviceItems[1].DeviceItems[subNode];
            NetworkInterface itf = deviceItem.GetService<NetworkInterface>();
            Node node = itf.Nodes[0];
            //按名称获取项目子网
            Subnet subnet = _project.Subnets.Find("PN/IE_1");
            //将设备子网节点连接到"PN/IE_1"子网
            node.ConnectToSubnet(subnet);
            //将IO连接器连接到IO系统
            itf.IoConnectors.First().ConnectToIoSystem(IoSystem());
            itf.IoConnectors.First().GetIoController();
        }

        /// <summary>
        /// 获取IO系统
        /// </summary>
        /// <returns>返回IO系统</returns>
        private static IoSystem IoSystem()
        {
            foreach (DeviceItem deviceItem in _deviceItemComposition)
            {
                switch (deviceItem.Name)
                {
                    case "PROFINET interface_1":
                    case "PROFINET 接口_1":
                    {
                        //获取PROFINET接口[X1]的IO系统
                        NetworkInterface itf = deviceItem.GetService<NetworkInterface>();
                        IoControllerComposition ioControllers = itf.IoControllers;
                        IoController ioController = ioControllers[0];
                        IoSystem ioSystem = ioController.IoSystem;

                        return ioSystem;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 配置起始地址
        /// </summary>
        /// <param name="ioDef"></param>
        /// <param name="startAddress"></param>
        /// <param name="addressComposition"></param>
        private static void SetStartAddress(IODef ioDef, int startAddress, AddressComposition addressComposition)
        {
            foreach (Address address in addressComposition) //轮询AddressComposition，查询设备项有几个地址（DI/DO/DIO）
            {
                var addressInfos = address.GetAttributeInfos();
                //轮询IO模块的地址属性
                foreach (EngineeringAttributeInfo addressInfo in addressInfos)
                {
                    //当StartAddress起始地址的权限为读写权限时，才允许给IO模块的地址赋值。
                    if (addressInfo.Name == "StartAddress" &&
                        addressInfo.AccessMode == EngineeringAttributeAccessMode.ReadWrite)
                    {
                        switch (address.IoType)
                        {
                            //获取AddressIoType用来判断IO模块是输入还是输出模块，写入对应输入或输出值
                            case AddressIoType.Input:
                                address.StartAddress =
                                    ioDef != null ? Convert.ToInt32(ioDef.InputStartAddress) : startAddress;
                                break;
                            case AddressIoType.Output:
                                address.StartAddress =
                                    ioDef != null ? Convert.ToInt32(ioDef.OutputStartAddress) : startAddress;
                                break;
                            case AddressIoType.None:
                            case AddressIoType.Substitute:
                            case AddressIoType.Diagnosis:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }

        #endregion

        #region Controller

        /// <summary>
        /// 控制器IO组态
        /// </summary>
        /// <param name="ioDef"></param>
        private static void ControllerIo(IODef ioDef)
        {
            //获取设备项的集合
            DeviceItemComposition deviceItemComposition = _deviceItemComposition;
            //轮询设备项的集合
            foreach (DeviceItem deviceItem in deviceItemComposition)
            {
                //插入GSD设备项
                if (deviceItem.CanPlugNew(ioDef.TypeIdentifier, ioDef.DeviceName, deviceItemComposition.Count))
                {
                    DeviceItem newPluggedDeviceItem = deviceItem.PlugNew(ioDef.TypeIdentifier, ioDef.DeviceName,
                        deviceItemComposition.Count);
                    //获取IO地址集合
                    AddressComposition addressComposition = newPluggedDeviceItem.DeviceItems.First().Addresses;
                    //设置IO起始地址
                    SetStartAddress(ioDef, 0, addressComposition);
                }
            }
        }

        #endregion

        #region Distributed I/O

        /// <summary>
        /// 创建ET200SP耦合模块
        /// </summary>
        /// <param name="ioDef"></param>
        private static void SIE_ET200SP(IODef ioDef)
        {
            //创建设备
            if (!string.IsNullOrEmpty(ioDef.TypeIdentifier))
            {
                _interfaceModule = ioDef.DeviceName;
                _project.Devices.CreateWithItem(ioDef.TypeIdentifier, ioDef.DeviceName, ioDef.DeviceName);
            }

            //设置子网和IO系统
            Subnet(ioDef.DeviceName, 1);
        }

        /// <summary>
        /// 创建ET200MP耦合模块
        /// </summary>
        /// <param name="ioDef"></param>
        private static void SIE_ET200MP(IODef ioDef)
        {
            //创建设备
            if (!string.IsNullOrEmpty(ioDef.TypeIdentifier))
            {
                _interfaceModule = ioDef.DeviceName;
                _project.Devices.CreateWithItem(ioDef.TypeIdentifier, ioDef.DeviceName, ioDef.DeviceName);
            }

            //设置子网和IO系统
            Subnet(ioDef.DeviceName, 0);
        }

        /// <summary>
        /// 分布式IO设备项组态
        /// </summary>
        /// <param name="ioDef"></param>
        private static void DistributedIoSlv(IODef ioDef)
        {
            //获取设备项的集合
            DeviceItemComposition deviceItemComposition = _project.Devices.Find(_interfaceModule).DeviceItems;
            //轮询设备项的集合
            foreach (DeviceItem deviceItem in deviceItemComposition)
            {
                //查询是否可以在这个插槽上插入设备项
                if (deviceItem.CanPlugNew(ioDef.TypeIdentifier, ioDef.DeviceName, deviceItemComposition.Count))
                {
                    DeviceItem newPluggedDeviceItem = deviceItem.PlugNew(ioDef.TypeIdentifier, ioDef.DeviceName,
                        deviceItemComposition.Count);
                    //获取IO地址集合
                    AddressComposition addressComposition = newPluggedDeviceItem.DeviceItems.First().Addresses;
                    //设置IO起始地址
                    SetStartAddress(ioDef, 0, addressComposition);
                }
            }
        }

        #endregion

        #region Other field devices

        /// <summary>
        /// 其他现场设备组态
        /// </summary>
        /// <param name="ioDef"></param>
        private static void OtherFieldDevices(IODef ioDef)
        {
            //创建机架
            if (!string.IsNullOrEmpty(ioDef.GsdId))
            {
                Device device = _project.Devices.Create(ioDef.GsdId, ioDef.DeviceName);
                //插入插槽
                if (device.CanPlugNew(ioDef.RackId, "Rack", 0))
                    device.PlugNew(ioDef.RackId, "Rack", 0);
                //在插槽上插入PN模块
                foreach (DeviceItem deviceItem in device.DeviceItems)
                {
                    //查询是否可以在这个插槽0位置上插入PN模块
                    if (deviceItem.CanPlugNew(ioDef.TypeId, ioDef.DeviceName, 0))
                        deviceItem.PlugNew(ioDef.TypeId, ioDef.DeviceName, 0);
                }
            }
        }

        /// <summary>
        /// 魏德米勒UR20组态
        /// </summary>
        /// <param name="ioDef"></param>
        private static void WeidmullerIo(IODef ioDef)
        {
            _interfaceModule = ioDef.DeviceName;
            OtherFieldDevices(ioDef); //其他现场设备组态
            Subnet(ioDef.DeviceName, 0); //设置子网和IO系统
        }

        /// <summary>
        /// 魏德米勒UR20 SLV组态
        /// </summary>
        private static void WeidmullerIoSlv(IODef ioDef)
        {
            //获取设备项的集合
            DeviceItemComposition deviceItemComposition = _project.Devices.Find(_interfaceModule).DeviceItems;
            //轮询设备项的集合
            foreach (DeviceItem deviceItem in deviceItemComposition)
            {
                //查询是否可以在这个插槽上插入设备项
                if (deviceItem.CanPlugNew(ioDef.Gsd + ioDef.TypeId, ioDef.DeviceName, deviceItemComposition.Count))
                {
                    DeviceItem newPluggedDeviceItem = deviceItem.PlugNew(ioDef.Gsd + ioDef.TypeId, ioDef.DeviceName,
                        deviceItemComposition.Count);
                    //获取IO地址集合
                    AddressComposition addressComposition = newPluggedDeviceItem.DeviceItems.First().Addresses;
                    //设置IO起始地址
                    SetStartAddress(ioDef, 0, addressComposition);
                }
            }
        }

        /// <summary>
        /// 康耐视DM260组态
        /// </summary>
        /// <param name="ioDef"></param>
        private static void Cognex(IODef ioDef)
        {
            if (!string.IsNullOrEmpty(ioDef.GsdId))
            {
                //创建GSD机架
                Device device = _project.Devices.Create(ioDef.GsdId, ioDef.DeviceName);
                //插入插槽
                if (device.CanPlugNew(ioDef.RackId, "RackId", 0))
                {
                    device.PlugNew(ioDef.RackId, "RackId", 0);
                }

                //轮询GSD设备的设备项
                foreach (DeviceItem deviceItem in device.DeviceItems)
                {
                    //在插槽上插入PN模块
                    if (deviceItem.CanPlugNew(ioDef.TypeId, ioDef.DeviceName, 0))
                    {
                        deviceItem.PlugNew(ioDef.TypeId, ioDef.DeviceName, 0);
                    }

                    //删除“结果数据64字节”，改为插入“结果数据16字节”
                    DeviceItem resultData64Bytes = device.DeviceItems[8];
                    resultData64Bytes.Delete();
                    const string gsdId = "GSD:GSDML-V2.31-COGNEX-DATAMAN-20170215.XML/M/401";
                    const string name = "Result Data - 16 bytes";
                    if (deviceItem.CanPlugNew(gsdId, name, 7))
                    {
                        deviceItem.PlugNew(gsdId, name, 7);
                    }
                }

                //连接子网和IO系统
                Subnet(ioDef.DeviceName, 0);
                //获取IO起始地址
                int[] arrayAddress =
                {
                    0,
                    0,
                    _scannerCount * 150 + 1900, //Q2000
                    _scannerCount * 150 + 1900, //I2000-2002
                    _scannerCount * 150 + 1903, //Q2003
                    _scannerCount * 150 + 1903, //I2003
                    _scannerCount * 150 + 1904, //I2004,Q2004
                    _scannerCount * 150 + 1905, //Q2005
                    _scannerCount * 150 + 1905, //I2005
                };
                //设置IO起始地址
                for (int i = 2; i <= 8; i++)
                {
                    //获取IO地址集合
                    AddressComposition addressComposition = device.DeviceItems[i].DeviceItems.First().Addresses;
                    //设置IO起始地址
                    SetStartAddress(null, arrayAddress[i], addressComposition);
                }
            }
        }

        /// <summary>
        /// 杰曼科技称重器组态
        /// </summary>
        /// <param name="ioDef"></param>
        private static void WeighScale(IODef ioDef)
        {
            //其他现场设备组态
            OtherFieldDevices(ioDef);
            //连接子网和IO系统
            Subnet(ioDef.DeviceName, 0);
            //获取IO地址集合
            AddressComposition addressComposition = _project.Devices.Find(ioDef.DeviceName).DeviceItems[1]
                .DeviceItems[2]
                .DeviceItems[0].Addresses;
            //设置IO起始地址
            int startAddress = _scalesCount * 5 + 2995;
            SetStartAddress(null, startAddress, addressComposition);
        }

        /// <summary>
        /// 吉诺IO组态
        /// </summary>
        /// <param name="ioDef"></param>
        private static void GseeIo(IODef ioDef)
        {
            //其他现场设备组态
            OtherFieldDevices(ioDef);
            //连接子网和IO系统
            Subnet(ioDef.DeviceName, 0);
            //获取IO起始地址
            int[] arrayAddress =
            {
                0, 0,
                Convert.ToInt32(ioDef.InputStartAddress),
                Convert.ToInt32(ioDef.InputStartAddress) + 1,
                Convert.ToInt32(ioDef.OutputStartAddress),
                Convert.ToInt32(ioDef.OutputStartAddress) + 1
            };
            //设置IO起始地址
            for (int i = 2; i <= 5; i++)
            {
                //获取IO地址集合
                AddressComposition addressComposition = _project.Devices.Find(ioDef.DeviceName).DeviceItems[1]
                    .DeviceItems[i].DeviceItems.First().Addresses;
                //设置IO起始地址
                SetStartAddress(null, arrayAddress[i], addressComposition);
            }
        }

        #endregion
    }
}