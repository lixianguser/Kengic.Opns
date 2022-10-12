using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Kengic.Opns.Database
{
    public class DataSetting
    {
        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="userId">用户名</param>
        /// <param name="newPwd">新密码</param>
        /// <returns>重置密码的结果</returns>
        public static bool Password(string userId,string newPwd)
        {
            string cmdText = "";
            SqlCommand cmd = null;
            //打开数据库
            Connection connection = new Connection();
            connection.Open();
            //使用管理员修改用户密码
            cmdText = Command.User.ResetPassword(userId, newPwd);
            cmd = new SqlCommand(cmdText, connection.SqlConnection);
            cmd.ExecuteNonQuery();
            //更新数据库密码数据
            cmdText = Command.User.UpdatePassword(userId, newPwd);
            cmd = new SqlCommand(cmdText, connection.SqlConnection);
            cmd.ExecuteNonQuery();
            //关闭数据库
            connection.Close();

            return true;
        }

        /// <summary>
        /// 更新最后登录时间
        /// </summary>
        /// <param name="sqlConnection">数据库连接</param>
        /// <param name="userId">用户名</param>
        public static void UpdateTime(SqlConnection sqlConnection,string userId)
        {
            string timeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//统计最后登录时间到数据库
            string cmdText = Command.User.UpdateTime(userId, timeNow);//数据库查询语句
            //执行数据库命令
            SqlCommand cmd = new SqlCommand(cmdText, sqlConnection);
            cmd.ExecuteNonQuery();
        }
        
    }
}