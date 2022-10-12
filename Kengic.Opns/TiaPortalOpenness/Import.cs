using System;
using System.Collections.Generic;
using System.IO;
using Siemens.Engineering;
using Siemens.Engineering.Hmi;
using Siemens.Engineering.Hmi.Communication;
using Siemens.Engineering.Hmi.Cycle;
using Siemens.Engineering.Hmi.Globalization;
using Siemens.Engineering.Hmi.RuntimeScripting;
using Siemens.Engineering.Hmi.Screen;
using Siemens.Engineering.Hmi.Tag;
using Siemens.Engineering.Hmi.TextGraphicList;
using Siemens.Engineering.SW;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.SW.ExternalSources;
using Siemens.Engineering.SW.Tags;
using Siemens.Engineering.SW.Types;

namespace Kengic.Opns.TiaPortalOpenness
{
    public partial class OpennessHelper
    {
         /// <summary>
        /// 导入PLC数据
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="filePath"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void Import(IEngineeringCompositionOrObject destination, string filePath)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination), "Parameter is null");
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Parameter is null or empty", nameof(filePath));

            FileInfo fileInfo = new FileInfo(filePath);
            const ImportOptions importOption = ImportOptions.Override;
            filePath = fileInfo.FullName;


            switch (destination)
            {
                case CycleComposition _:
                case ConnectionComposition _:
                case MultiLingualGraphicComposition _:
                case GraphicListComposition _:
                case TextListComposition _:
                {
                    var parameter = new Dictionary<Type, object>();
                    parameter.Add(typeof(string), filePath);
                    parameter.Add(typeof(ImportOptions), importOption);
                    // Export prüfen
                    (destination as IEngineeringComposition).Invoke("Import", parameter);
                    break;
                }
                case PlcBlockGroup group when Path.GetExtension(filePath).Equals(".xml"):
                    group.Blocks.Import(fileInfo, importOption);
                    break;
                case PlcBlockGroup _:
                {
                    IEngineeringObject currentDestination = destination as IEngineeringObject;
                    while (!(currentDestination is PlcSoftware))
                    {
                        currentDestination = currentDestination.Parent;
                    }

                    PlcExternalSourceComposition col = (currentDestination as PlcSoftware).ExternalSourceGroup
                        .ExternalSources;

                    string sourceName = Path.GetRandomFileName();
                    sourceName = Path.ChangeExtension(sourceName, ".src");
                    PlcExternalSource src = col.CreateFromFile(sourceName, filePath);
                    src.GenerateBlocksFromSource();
                    src.Delete();
                    break;
                }
                case PlcTagTableGroup group:
                    group.TagTables.Import(fileInfo, importOption);
                    break;
                case PlcTypeGroup group:
                    group.Types.Import(fileInfo, importOption);
                    break;
                case PlcExternalSourceGroup group:
                {
                    PlcExternalSource temp = group.ExternalSources.Find(Path.GetFileName(filePath));
                    if (temp != null)
                        temp.Delete();
                    group.ExternalSources.CreateFromFile(Path.GetFileName(filePath), filePath);
                    break;
                }
                case TagFolder folder:
                    folder.TagTables.Import(fileInfo, importOption);
                    break;
                case ScreenFolder folder:
                    folder.Screens.Import(fileInfo, importOption);
                    break;
                case ScreenTemplateFolder folder:
                    folder.ScreenTemplates.Import(fileInfo, importOption);
                    break;
                case ScreenPopupFolder folder:
                    folder.ScreenPopups.Import(fileInfo, importOption);
                    break;
                case ScreenSlideinSystemFolder folder:
                    folder.ScreenSlideins.Import(fileInfo, importOption);
                    break;
                case VBScriptFolder folder:
                    folder.VBScripts.Import(fileInfo, importOption);
                    break;
                case ScreenGlobalElements _:
                    (destination.Parent as HmiTarget)?.ImportScreenGlobalElements(fileInfo, importOption);
                    break;
                case ScreenOverview _:
                    (destination.Parent as HmiTarget)?.ImportScreenOverview(fileInfo, importOption);
                    break;
            }
        }
         
    }
}