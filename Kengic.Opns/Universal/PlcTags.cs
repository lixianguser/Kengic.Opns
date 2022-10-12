using System.Collections.Generic;
using System.IO;
using Kengic.Opns.Database;
using Kengic.Opns.Utility;
using Kengic.Opns.Xml;
using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.SW.Tags;

namespace Kengic.Opns.Universal
{
    public class PlcTags
    {
        private static TiaPortal _tiaPortal => AddInProvider._tiaPortal;

        public static void Import_Execution(MenuSelectionProvider<PlcTagTableSystemGroup> menuSelectionProvider)
        {
            PlcTarget plcTarget = new PlcTarget(menuSelectionProvider.GetSelection());
            using (ExclusiveAccess exclusiveAccess = _tiaPortal.ExclusiveAccess("导入中..."))
            {
                using (Transaction transaction =
                       exclusiveAccess.Transaction(plcTarget.Project, "导入变量"))
                {
                    Sheet sheet = new Sheet(plcTarget.ConfigFilePath);
                    var IoMaps = new List<IOMap>();
                    for (int i = 1; i < sheet.IoMap_Row.Count; i++)
                    {
                        sheet.GetIoMap(i);
                        IOMap ioMap = new IOMap
                        {
                            symbol = sheet.Symbol.Value,
                            comment = sheet.Comment.Value,
                            address = sheet.Address.Value,
                            dataType = sheet.DataType.Value,
                            location = sheet.IoMap_Location.Value
                        };
                        IoMaps.Add(ioMap);
                    }

                    PlcTagTables plcTagTables = new PlcTagTables(IoMaps, plcTarget.Version);
                    string saveFilePath = Path.Combine(plcTarget.ExportDirectory, "plcTags.xml");
                    plcTagTables.XmlDocument.Save(saveFilePath);
                    FileInfo fileInfo = new FileInfo(saveFilePath);
                    foreach (PlcTagTableSystemGroup plcTagTableSystemGroup in menuSelectionProvider
                                 .GetSelection())
                    {
                        exclusiveAccess.Text = "导入变量表";
                        //从xml导入变量表
                        plcTagTableSystemGroup.TagTables.Import(fileInfo, ImportOptions.Override);
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