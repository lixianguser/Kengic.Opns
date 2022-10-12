using System;
using System.Diagnostics;
using System.Windows.Forms;
using Siemens.Engineering;
using Siemens.Engineering.HW;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.SW.Tags;
using Siemens.Engineering.AddIn.Menu;

namespace Kengic.Opns.Universal
{
    public class AddIn : ContextMenuAddIn
    {
        private readonly TiaPortal _tiaPortal;

        public AddIn(TiaPortal tiaPortal) : base("通用")
        {
            _tiaPortal = tiaPortal;
        }

        protected override void BuildContextMenuItems(ContextMenuAddInRoot rootSubmenu)
        {
            //子菜单
            Submenu deviceAndNetworksSubmenu = rootSubmenu.Items.AddSubmenu("设备和网络");
            Submenu plcTagsSubmenu = rootSubmenu.Items.AddSubmenu("PLC变量");
            Submenu toolSubmenu = rootSubmenu.Items.AddSubmenu("工具");
            Submenu propertiesSubmenu = rootSubmenu.Items.AddSubmenu("属性");

            //"设备和网络" 简单按钮
            deviceAndNetworksSubmenu.Items.AddActionItem<DeviceItem>("导入组态", ImportDevice_OnClick);
            deviceAndNetworksSubmenu.Items.AddActionItem<IEngineeringObject>("导入组态",
                menuSelectionProvider => { }, IsDeviceItem);
            deviceAndNetworksSubmenu.Items.AddActionItem<IEngineeringObject>("支持的硬件目录", Hardware_OnClick);
            deviceAndNetworksSubmenu.Items.AddActionItem<Project>("导出网络信息", ExportNetworkInfo_OnClick);
            deviceAndNetworksSubmenu.Items.AddActionItem<IEngineeringObject>("导出网络信息",
                menuSelectionProvider => { }, IsProject);

            //"PLC变量" 简单按钮
            plcTagsSubmenu.Items.AddActionItem<PlcTagTableSystemGroup>("导入变量", ImportTags_OnClick);
            plcTagsSubmenu.Items.AddActionItem<IEngineeringObject>("导入变量",
                menuSelectionProvider => { }, IsPlcTagTableSystemGroup);

            //"工具" 简单按钮
            toolSubmenu.Items.AddActionItem<IEngineeringObject>("导出Xml", ExportXml_OnClick);
            toolSubmenu.Items.AddActionItem<IEngineeringObject>("导入Xml", ImportXml_OnClick);
            toolSubmenu.Items.AddActionItem<InstanceDB>("PLC导入SCADA", PlcImportScada_OnClick);
            toolSubmenu.Items.AddActionItem<IEngineeringObject>("PLC导入SCADA FB",
                menuSelectionProvider => { }, IsInstanceDB);
            toolSubmenu.Items.AddActionItem<IEngineeringObject>("HMI导入SCADA", HmiImportScada_OnClick);

            //"属性" 简单按钮
            propertiesSubmenu.Items.AddActionItem<PlcBlock>("重新编号", Number_OnClick);
            propertiesSubmenu.Items.AddActionItem<IEngineeringObject>("重新编号",
                menuSelectionProvider => { }, IsPlcBlock);
            propertiesSubmenu.Items.AddActionItem<InstanceDB>("指定ProDiag FB", Assignment_OnClick);
            propertiesSubmenu.Items.AddActionItem<IEngineeringObject>("指定ProDiag FB",
                menuSelectionProvider => { }, IsInstanceDB);
            propertiesSubmenu.Items.AddActionItem<DataBlock>("设置块保持特性", SetRetain_OnClick);
            propertiesSubmenu.Items.AddActionItem<IEngineeringObject>("设置块保持特性",
                menuSelectionProvider => { }, IsDataBlock);
        }

        #region 设备和网络

        /// <summary>
        /// 导入硬件组态
        /// </summary>
        /// <param name="menuSelectionProvider">选中的PLC设备</param>
        private static void ImportDevice_OnClick(MenuSelectionProvider<DeviceItem> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                DevicesAndNetworks.Import(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 支持的设备类型列表
        /// </summary>
        /// <param name="menuSelectionProvider"></param>
        private static void Hardware_OnClick(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                DevicesAndNetworks.Hardware(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导出项目网络数据
        /// </summary>
        /// <param name="menuSelectionProvider">选中的项目</param>
        private static void ExportNetworkInfo_OnClick(MenuSelectionProvider<Project> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                DevicesAndNetworks.ExportNetworkInfo(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region PLC变量

        /// <summary>
        /// 导入PLC变量
        /// </summary>
        /// <param name="menuSelectionProvider">选中PLC变量表组</param>
        private static void ImportTags_OnClick(MenuSelectionProvider<PlcTagTableSystemGroup> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                PlcTags.Import(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 工具

        /// <summary>
        /// 导出程序数据Xml文件
        /// </summary>
        /// <param name="menuSelectionProvider">选中的对象</param>
        private static void ExportXml_OnClick(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                Tools.Export(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导入程序数据Xml文件
        /// </summary>
        /// <param name="menuSelectionProvider">选中的组对象</param>
        private static void ImportXml_OnClick(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                Tools.Import(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// PLC导入SCADA数据块和FC块
        /// </summary>
        /// <param name="menuSelectionProvider"></param>
        private static void PlcImportScada_OnClick(MenuSelectionProvider<InstanceDB> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                Tools.PlcImportScada(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// HMI导入SCADA报警变量和生成报警文本
        /// </summary>
        /// <param name="menuSelectionProvider"></param>
        private static void HmiImportScada_OnClick(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                Tools.HmiImportScada(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 将程序块重新编号
        /// </summary>
        /// <param name="menuSelectionProvider">选中的对象</param>
        private static void Number_OnClick(MenuSelectionProvider<PlcBlock> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                Property.Number(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 指定实例背景数据块的ProDiag程序块
        /// </summary>
        /// <param name="menuSelectionProvider"></param>
        private static void Assignment_OnClick(MenuSelectionProvider<InstanceDB> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                Property.Assignment(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 将DB块变量设置为具有保持性
        /// </summary>
        /// <param name="menuSelectionProvider">选中的数据块</param>
        private static void SetRetain_OnClick(MenuSelectionProvider<DataBlock> menuSelectionProvider)
        {
#if DEBUG
            Debugger.Launch();
#endif
            try
            {
                Property.SetRetain(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 菜单功能使能

        private static MenuStatus IsDeviceItem(MenuSelectionProvider menuSelectionProvider)
        {
            var show = false;

            foreach (IEngineeringObject engineeringObject in menuSelectionProvider.GetSelection())
            {
                if (!(engineeringObject is DeviceItem))
                {
                    show = true;
                    break;
                }
            }

            return show ? MenuStatus.Disabled : MenuStatus.Hidden;
        }

        private static MenuStatus IsProject(MenuSelectionProvider menuSelectionProvider)
        {
            var show = false;

            foreach (IEngineeringObject engineeringObject in menuSelectionProvider.GetSelection())
            {
                if (!(engineeringObject is Project))
                {
                    show = true;
                    break;
                }
            }

            return show ? MenuStatus.Disabled : MenuStatus.Hidden;
        }

        private static MenuStatus IsPlcTagTableSystemGroup(MenuSelectionProvider menuSelectionProvider)
        {
            bool show = false;

            foreach (IEngineeringObject engineeringObject in menuSelectionProvider.GetSelection())
            {
                if (!(engineeringObject is PlcTagTableSystemGroup))
                {
                    show = true;
                    break;
                }
            }

            return show ? MenuStatus.Disabled : MenuStatus.Hidden;
        }

        private static MenuStatus IsPlcBlock(MenuSelectionProvider menuSelectionProvider)
        {
            bool show = false;

            foreach (IEngineeringObject engineeringObject in menuSelectionProvider.GetSelection())
            {
                if (!(engineeringObject is PlcBlock))
                {
                    show = true;
                    break;
                }
            }

            return show ? MenuStatus.Disabled : MenuStatus.Hidden;
        }

        private static MenuStatus IsInstanceDB(MenuSelectionProvider menuSelectionProvider)
        {
            var show = false;

            foreach (IEngineeringObject engineeringObject in menuSelectionProvider.GetSelection())
            {
                if (!(engineeringObject is InstanceDB))
                {
                    show = true;
                    break;
                }
            }

            return show ? MenuStatus.Disabled : MenuStatus.Hidden;
        }

        private static MenuStatus IsDataBlock(MenuSelectionProvider menuSelectionProvider)
        {
            var show = false;

            foreach (IEngineeringObject engineeringObject in menuSelectionProvider.GetSelection())
            {
                if (!(engineeringObject is DataBlock))
                {
                    show = true;
                    break;
                }
            }

            return show ? MenuStatus.Disabled : MenuStatus.Hidden;
        }

        #endregion
    }
}