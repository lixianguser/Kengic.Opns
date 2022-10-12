namespace Kengic.Opns.Database
{
    public class Command
    {
        /// <summary>
        /// 处理数据库标签
        /// </summary>
        /// <param name="variable">变量名称</param>
        /// <returns>'variable'</returns>
        private static string Tag(string variable)
        {
            string tag = string.Concat("'", variable, "'");
            return tag;
        }

        public class User
        {
            /// <summary>
            /// 设置密码
            /// </summary>
            /// <param name="userId">用户名</param>
            /// <param name="newPwd">新密码</param>
            /// <returns>EXEC[sp_password]NULL,'password','userId'</returns>
            public static string ResetPassword(string userId, string newPwd)
            {
                string cmdText = "EXEC[sp_password]NULL," + Tag(newPwd) + "," + Tag(userId);
                return cmdText;
            }

            /// <summary>
            /// 更新数据库用户密码数据
            /// </summary>
            /// <param name="userId">用户名</param>
            /// <param name="newPwd">新密码</param>
            /// <returns>"UPDATE[user]SET[Password]='password'WHERE[Login]='userId'</returns>
            public static string UpdatePassword(string userId, string newPwd)
            {
                string cmdText = string.Concat("UPDATE[user]SET[Password]=", Tag(newPwd), "WHERE[Login]=", Tag(userId));
                return cmdText;
            }

            /// <summary>
            /// 更新数据库指定用户最后登录时间一栏数据
            /// </summary>
            /// <param name="userId">用户名</param>
            /// <param name="dateTime">日期时间</param>
            /// <returns>"UPDATE[user]SET[LastLogged]='dateTime'WHERE[Login]='userId'</returns>
            public static string UpdateTime(string userId, string dateTime)
            {
                string cmdText = "UPDATE[user]SET[LastLogged]=" + Tag(dateTime) + "WHERE[Login]=" + Tag(userId);
                return cmdText;
            }
        }

        public class IO_DEF
        {
            /// <summary>
            /// 获取对应'IoDevice'名称的数据
            /// </summary>
            /// <param name="IoDevice">硬件组态设备名称</param>
            /// <returns>"SELECT*FROM[IO DEF]WHERE[IoDevice]='IoDevice'";</returns>
            public static string Where(string IoDevice)
            {
                string cmdText = string.Concat("SELECT*FROM[IO DEF]WHERE[IoDevice]=",Tag(IoDevice));
                return cmdText;
            }
            
            /// <summary>
            /// 获取IoDef全部数据
            /// </summary>
            /// <returns>"SELECT*FROM[IO DEF]"</returns>
            public static string GetAll()
            {
                string cmdText = "SELECT*FROM[IO DEF]";
                return cmdText;
            }
        }
    }
}