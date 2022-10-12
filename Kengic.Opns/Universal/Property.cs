using System;
using System.IO;
using System.Windows.Forms;
using Kengic.Opns.Database;
using Kengic.Opns.TiaPortalOpenness;
using Kengic.Opns.Utility;
using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.SW.Blocks;

namespace Kengic.Opns.Universal
{
    public class Property
    {
        private static TiaPortal _tiaPortal => AddInProvider._tiaPortal;

        public static void Number(MenuSelectionProvider<PlcBlock> menuSelectionProvider)
        {
            PlcTarget plcTarget = new PlcTarget(menuSelectionProvider.GetSelection());
            plcTarget.GoOffline();
            //TODO 新建一个NumberForm
            // NumberForm numberForm = new NumberForm();
            // NumberForm.DataMember = "StandardNumberingRules";
            Connection connection = new Connection();
            connection.Open();
            //TODO 获取编号规则表数据
            //DataSet dataSet = DataReader.GetNumberRulesFull(NumberForm.DataMember);
            //NumberForm.DataSet = dataSet;
            //TODO 置顶打开打开窗体
            // if (numberForm.ShowDialog(UI.Top()) == DialogResult.OK)
            // {
            using (ExclusiveAccess exclusiveAccess = _tiaPortal.ExclusiveAccess("重新编号中..."))
            {
                //新建一个回滚描述
                using (Transaction transaction =
                       exclusiveAccess.Transaction(plcTarget.Project, "重新编号"))
                {
                    foreach (PlcBlock plcBlock in menuSelectionProvider.GetSelection())
                    {
                        exclusiveAccess.Text = "为 " + plcBlock.Name + " 重新编号...";
                        //关闭自动编号功能
                        if (plcBlock.AutoNumber)
                            plcBlock.AutoNumber = false;
                        //TODO 写入编号
                        // if (plcBlock.Number != startingNumber) 
                        //     plcBlock.Number = startingNumber; 
                        //TODO 递增编号
                        //startingNumber += incrementNumber; 
                    }

                    if (transaction.CanCommit)
                    {
                        transaction.CommitOnDispose();
                    }
                }
            }
        }

        public static void Assignment(MenuSelectionProvider<InstanceDB> menuSelectionProvider)
        {
            PlcTarget plcTarget = new PlcTarget(menuSelectionProvider.GetSelection());
            //TODO 获取PLC程序中所有语言为plcBlock.ProgrammingLanguage.ProDiag
            //TODO 新建一个设定对话框
            // if (assignmentOfProDiagFBsForm.ShowDialog(UI.Top()) == DialogResult.OK)
            // {
            //TODO 获取来自选中的ProDiagFB块名称
            //string proDiagFb = assignmentOfProDiagFBsForm.SelectedValueText; 

            using (ExclusiveAccess exclusiveAccess = _tiaPortal.ExclusiveAccess("指定 ProDiag FB..."))
            {
                using (Transaction transaction =
                       exclusiveAccess.Transaction(plcTarget.Project, "指定 ProDiag FB"))
                {
                    foreach (InstanceDB instanceDB in menuSelectionProvider.GetSelection())
                    {
                        object attribute = instanceDB.GetAttribute("AssignedProDiagFB").ToString();
                        //TODO 指定AssignedProDiagFB
                        //指定AssignedProDiagFB
                        // if (attribute == proDiagFb)
                        // {
                        //     exclusiveAccess.Text = "为" + instanceDB.Name + "指定ProDiag FB...";
                        //     instanceDB.SetAttribute("AssignedProDiagFB", proDiagFb);
                        // }
                    }

                    if (transaction.CanCommit)
                    {
                        transaction.CommitOnDispose();
                    }
                }
            }
        }

        public static void SetRetain(MenuSelectionProvider<DataBlock> menuSelectionProvider)
        {
            PlcTarget plcTarget = new PlcTarget(menuSelectionProvider.GetSelection());
            using (ExclusiveAccess exclusiveAccess = _tiaPortal.ExclusiveAccess("设置块保持特性..."))
            {
                using (Transaction transaction =
                       exclusiveAccess.Transaction(plcTarget.Project, "设置块保持特性"))
                {
                    foreach (DataBlock dataBlock in menuSelectionProvider.GetSelection())
                    {
                        string filePath = Path.Combine(plcTarget.ExportDirectory, dataBlock.Name + ".xml");
                        exclusiveAccess.Text = "为 " + dataBlock.Name + " 设置块保持特性...";
                        OpennessHelper.Export(dataBlock, filePath);
                        //修改Block Xml文件中的Retain节点
                        Xml.InstanceDB block = new Xml.InstanceDB(filePath);
                        block.SetRemanence();
                        block.XmlDocument.Save(filePath);
                        OpennessHelper.Import(dataBlock.Parent, filePath);
                        File.Delete(filePath);
                    }

                    if (transaction.CanCommit)
                    {
                        transaction.CommitOnDispose();
                    }
                }
            }
        }
    }
}