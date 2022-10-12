using System.Data;
using System.Data.SqlClient;

namespace Kengic.Opns.Database
{
    /// <summary>
    /// Kengic.Openness 数据库连接数据
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string Server;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password;

        /// <summary>
        /// 可访问的数据库
        /// </summary>
        public string Database;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString;

        /// <summary>
        /// SQL的连接
        /// </summary>
        public SqlConnection SqlConnection;

        /// <summary>
        /// 建立一个新的连接
        /// </summary>
        public Connection()
        {
            Server = "139.224.22.57";
            UserId = User.Default.UserId;
            Password = User.Default.Password;
            Database = "Kengic";

            string str = string.Concat("server=", Server, ";"); //SQL服务器IP地址
            str += string.Concat("uid=", UserId, ";"); //SQL服务器用户名
            str += string.Concat("pwd=", Password, ";"); //SQL服务器密码
            str += string.Concat("database=", Database, ";"); //SQL服务器数据库名称
            ConnectionString = str;
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        public void Open()
        {
            SqlConnection = new SqlConnection(ConnectionString);
            if (SqlConnection.State == ConnectionState.Closed)
            {
                SqlConnection.Open(); //建立连接，可能出现异常,使用try catch语句
            }
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        public void Open(string userId, string password)
        {
            string str = string.Concat("server=", Server, ";"); //SQL服务器IP地址
            str += string.Concat("uid=", userId, ";"); //SQL服务器用户名
            str += string.Concat("pwd=", password, ";"); //SQL服务器密码
            str += string.Concat("database=", Database, ";"); //SQL服务器数据库名称
            ConnectionString = str;

            SqlConnection = new SqlConnection(ConnectionString);
            if (SqlConnection.State == ConnectionState.Closed)
            {
                SqlConnection.Open(); //建立连接，可能出现异常,使用try catch语句
            }
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void Close()
        {
            if (SqlConnection.State == ConnectionState.Closed)
            {
                SqlConnection.Open(); //建立连接，可能出现异常,使用try catch语句
            }
        }
    }
}