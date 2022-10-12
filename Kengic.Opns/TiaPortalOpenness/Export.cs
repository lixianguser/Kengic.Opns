using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Siemens.Engineering;
using Siemens.Engineering.Hmi.Communication;
using Siemens.Engineering.Hmi.Cycle;
using Siemens.Engineering.Hmi.Globalization;
using Siemens.Engineering.Hmi.RuntimeScripting;
using Siemens.Engineering.Hmi.Screen;
using Siemens.Engineering.Hmi.Tag;
using Siemens.Engineering.Hmi.TextGraphicList;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.SW.ExternalSources;
using Siemens.Engineering.SW.Tags;
using Siemens.Engineering.SW.Types;

namespace Kengic.Opns.TiaPortalOpenness
{
    public partial class OpennessHelper
    {
        /// <summary>
        /// 导出PLC数据
        /// </summary>
        /// <param name="exportItem"></param>
        /// <param name="exportPath"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="EngineeringException"></exception>
        /// <returns></returns>
        public static string Export(IEngineeringObject exportItem, string exportPath)
        {
            // plcBlock.Export(fileInfo,ExportOptions.WithDefaults);
            if (exportItem == null)
                throw new ArgumentNullException(nameof(exportItem), "Parameter is null");
            if (string.IsNullOrEmpty(exportPath))
                throw new ArgumentException("Parameter is null or empty", nameof(exportPath));

            // string filePath = Path.GetFullPath(exportPath);
            const ExportOptions exportOption = ExportOptions.WithDefaults;

            switch (exportItem)
            {
                case PlcBlock item:
                {
                    if (item.ProgrammingLanguage == ProgrammingLanguage.ProDiag ||
                        item.ProgrammingLanguage == ProgrammingLanguage.ProDiag_OB)
                        return null;
                    if (item.IsConsistent)
                    {
                        // filePath = Path.Combine(filePath, AdjustNames.AdjustFileName(GetObjectName(item)) + ".xml");
                        if (File.Exists(exportPath))
                        {
                            File.Delete(exportPath);
                        }

                        item.Export(new FileInfo(exportPath), exportOption);

                        return exportPath;
                    }

                    throw new EngineeringException(string.Format(CultureInfo.InvariantCulture,
                        "块: {0} 未编译! 导出失败!", item.Name));
                }
                case PlcTagTable _:
                case PlcType _:
                case ScreenOverview _:
                case ScreenGlobalElements _:
                case Screen _:
                case TagTable _:
                case Connection _:
                case GraphicList _:
                case TextList _:
                case Cycle _:
                case MultiLingualGraphic _:
                case ScreenTemplate _:
                case VBScript _:
                case ScreenPopup _:
                case ScreenSlidein _:
                {
                    // Directory.CreateDirectory(filePath);
                    // filePath = Path.Combine(filePath, AdjustNames.AdjustFileName(GetObjectName(exportItem)) + ".xml");
                    // File.Delete(filePath);
                    if (File.Exists(exportPath))
                    {
                        File.Delete(exportPath);
                    }

                    var parameter = new Dictionary<Type, object>
                    {
                        { typeof(FileInfo), new FileInfo(exportPath) },
                        { typeof(ExportOptions), exportOption }
                    };
                    exportItem.Invoke("Export", parameter);
                    return exportPath;
                }
                case PlcExternalSource _:
                    //Directory.CreateDirectory(filePath);
                    //filePath = Path.Combine(filePath, AdjustNames.AdjustFileName(GetObjectName(exportItem)));
                    //File.Delete(filePath);
                    //File.Create(filePath);
                    //return filePath;
                    break;
            }

            return null;
        }

    }
}