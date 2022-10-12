using System.IO;
using System.Linq;
using Siemens.Engineering;

namespace Kengic.Opns.Utility
{
    public class ProjectInfos
    {
        private readonly TiaPortal _tiaPortal;

        public Project Project;
        public string Name;
        public string FilePath;
        public string ConfigFilePath;
        public string ExportDirectory;
        public string ProjectDirectory;
        public string Version;
        public string CSVFilePth;

        public ProjectInfos()
        {
            _tiaPortal = AddInProvider._tiaPortal;
            Project = _tiaPortal?.Projects.First();
            Name = Project?.Name;
            FilePath = Project?.Path.FullName;
            ProjectDirectory = Project?.Path.DirectoryName;
            ConfigFilePath = Path.ChangeExtension(FilePath, "xml");
            ExportDirectory = GetExportDirectory();
            CSVFilePth = Path.ChangeExtension(FilePath, "csv");
            GetVersion();
        }

        /// <summary>
        /// 获取导出文件夹路径
        /// </summary>
        /// <returns>～\$(ProjectDirectory)\Export</returns>
        private string GetExportDirectory()
        {
            var ret = Path.Combine(ProjectDirectory, "Export");
            if (!Directory.Exists(ret)) Directory.CreateDirectory(ret);
            return ret;
        }

        private void GetVersion()
        {
            string extension = Path.GetExtension(FilePath);
            switch (extension)
            {
                case ".ap16":
                case ".amc16":
                    Version = "V16";
                    break;
                case ".ap17":
                case ".amc17":
                    Version = "V17";
                    break;
            }
        }
    }
}