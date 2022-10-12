using System.Data;
using System.Data.SqlClient;

namespace Kengic.Opns.Database
{
    public class DataReader
    {
        public class IO_DEF
        {
            /// <summary>
            /// 根据DAT数据获取数据库匹配数据
            /// </summary>
            /// <param name="ioDef"></param>
            /// <param name="connection">数据库链接</param>
            public static void Get(IODef ioDef,SqlConnection connection)
            {
                //数据库查询语句
                string cmdText = Command.IO_DEF.Where(ioDef.Device);
                SqlCommand cmd = new SqlCommand(cmdText, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ioDef.TypeIdentifier = reader.GetString(3);
                    ioDef.Gsd = reader.GetString(4);
                    ioDef.GsdId = reader.GetString(5);
                    ioDef.RackId = reader.GetString(6);
                    ioDef.TypeId = reader.GetString(7);
                }

                //关闭数据
                reader.Close();
            }
            
            /// <summary>
            /// 获取IoDef数据库全部数据
            /// </summary>
            /// <returns>DataSet</returns>
            public static DataSet Get(string dataMember,SqlConnection connection)
            {
                //数据库查询语句
                string cmdText = Command.IO_DEF.GetAll();
                //数据库命令
                SqlCommand cmd = new SqlCommand(cmdText, connection);
                //获取数据写入搭配DataSet
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, dataMember);

                return dataSet;
            }
        }
    }
    
    /// <summary>
    /// PLC设备组态
    /// </summary>
    public class IODef
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// IO输入设备的起始地址
        /// </summary>
        public string InputStartAddress { get; set; }

        /// <summary>
        /// IO输出设备的起始地址
        /// </summary>
        public string OutputStartAddress { get; set; }

        /// <summary>
        /// 设备名称 (e.g. CC1-A1)
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 类型标识符
        /// </summary>
        public string TypeIdentifier { get; set; }

        /// <summary>
        /// 第三方设备描述文件
        /// </summary>
        public string Gsd { get; set; }

        /// <summary>
        /// 第三方设备描述文件-机架
        /// </summary>
        public string GsdId { get; set; }

        /// <summary>
        /// 第三方设备描述文件-插槽
        /// </summary>
        public string RackId { get; set; }

        /// <summary>
        /// 第三方设备描述文件-PN模块
        /// </summary>
        public string TypeId { get; set; }
    }
    
    /// <summary>
    /// PLC变量
    /// </summary>
    public class IOMap
    {
        /// <summary>
        /// PLC 变量符号
        /// </summary>
        /// <value></value>
        public string symbol { get; set; }

        /// <summary>
        /// PLC 变量备注
        /// </summary>
        /// <value></value>
        public string comment { get; set; }

        /// <summary>
        /// PLC 变量地址
        /// </summary>
        /// <value></value>
        public string address { get; set; }

        /// <summary>
        /// 变量数据类型
        /// </summary>
        public string dataType { get; set; }

        /// <summary>
        /// 安装IO模块的电柜名称
        /// </summary>
        /// <value></value>
        public string location { get; set; }
    }
}