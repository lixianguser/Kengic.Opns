using System.IO;
using System.Windows.Forms;
using Kengic.Opns.Command;
using Kengic.Opns.Database;
using Kengic.Opns.TiaPortalOpenness;
using Kengic.Opns.Utility;
using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.SW.Blocks;

namespace Kengic.Opns.Universal
{
    public class Tools
    {
        private static TiaPortal _tiaPortal => AddInProvider._tiaPortal;
        
        public static void Export_Execution(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
            PlcTarget plcTarget = new PlcTarget(menuSelectionProvider.GetSelection());
            using (ExclusiveAccess exclusiveAccess = _tiaPortal.ExclusiveAccess("导出中..."))
            {
                using (Transaction transaction =
                       exclusiveAccess.Transaction(plcTarget.Project, "导出"))
                {
                    foreach (IEngineeringObject iEngineeringObject in menuSelectionProvider.GetSelection())
                    {
                        //查询名称是否包含“/”，如果包含替换更“_”
                        string name = iEngineeringObject.GetAttribute("Name").ToString();
                        while (name.Contains("/"))
                        {
                            name = name.Replace("/", "_");
                        }

                        //导出数据
                        string filePath = Path.Combine(plcTarget.ExportDirectory, name + ".xml");
                        exclusiveAccess.Text = "导出 " + name;
                        OpennessHelper.Export(iEngineeringObject, filePath);
                    }

                    if (transaction.CanCommit)
                    {
                        transaction.CommitOnDispose();
                    }
                }
            }
        }

        public static void Import_Execution(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
            PlcTarget plcTarget = new PlcTarget(menuSelectionProvider.GetSelection());
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "xml File(*.xml)| *.xml",
                InitialDirectory = plcTarget.ProjectDirectory
            };
            if (openFileDialog.ShowDialog(Command.UI.Top()) == DialogResult.OK
                && !string.IsNullOrEmpty(openFileDialog.FileName))
            {
                using (ExclusiveAccess exclusiveAccess = _tiaPortal.ExclusiveAccess("导入中..."))
                {
                    using (Transaction transaction =
                           exclusiveAccess.Transaction(plcTarget.Project, "导入"))
                    {
                        foreach (IEngineeringObject iEngineeringObject in menuSelectionProvider.GetSelection())
                        {
                            foreach (string fileName in openFileDialog.FileNames)
                            {
                                exclusiveAccess.Text = "导入 " + Path.GetFileName(fileName);
                                OpennessHelper.Import(iEngineeringObject, fileName);
                            }
                        }

                        if (transaction.CanCommit)
                        {
                            transaction.CommitOnDispose();
                        }
                    }
                }
            }
        }

        public static void PlcImportScada_Execution(MenuSelectionProvider<InstanceDB> menuSelectionProvider)
        {
            PlcTarget plcTarget = new PlcTarget(menuSelectionProvider.GetSelection());
            using (ExclusiveAccess exclusiveAccess = _tiaPortal.ExclusiveAccess("PLC导入SCADA..."))
            {
                using (Transaction transaction =
                       exclusiveAccess.Transaction(plcTarget.Project, "PLC导入SCADA"))
                {
                    Connection connection = new Connection();
                    connection.Open();
                    //TODO 从数据库获取Member
                    //Member member = new Member();
                    //TODO 从数据库获取StlStatement
                    //StlStatement stlStatement = new StlStatement();
                    //TODO 从数据库获取SCADA
                    if (transaction.CanCommit)
                    {
                        transaction.CommitOnDispose();
                    }
                }
            }
        }

        public static void HmiImportScada_Execution(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
            //TODO 触摸屏导入SCADA
            PlcTarget plcTarget = new PlcTarget(menuSelectionProvider.GetSelection());
            using (ExclusiveAccess exclusiveAccess = _tiaPortal.ExclusiveAccess("HMI导入SCADA..."))
            {
                using (Transaction transaction =
                       exclusiveAccess.Transaction(plcTarget.Project, "HMI导入SCADA"))
                {

                    if (transaction.CanCommit)
                    {
                        transaction.CommitOnDispose();
                    }
                }
            }
        }
    }
}