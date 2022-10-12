using System;
using System.Windows.Forms;
using Kengic.Opns.Database.UI;

namespace Kengic.Opns.Database
{
    /// <summary>
    /// 数据库管理
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        public static void Login()
        {
            if (!SessionValid())
            {
                LoginForm form = new LoginForm(); //新建一个登录窗体
                if (form.ShowDialog(Top()) == DialogResult.OK)
                {
                    //保存用户名和密码
                    User.Default.UserId = form.UserId;
                    User.Default.Password = form.Password;
                    User.Default.Save();

                    //打开数据库
                    Connection connection = new Connection();
                    connection.Open();

                    //判断是否为初始密码
                    string initPwd = string.Concat(User.Default.UserId, "123");
                    if (connection.Password != initPwd)
                    {
                        DataSetting.UpdateTime(connection.SqlConnection, User.Default.UserId);
                        User.Default.LoginState = true;
                        User.Default.LastLoginTime = DateTime.Now;
                        User.Default.Save();
                        MessageBox.Show("请重新激活插件", "登录成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ResetPassword();
                    }

                    //关闭数据库
                    connection.Close();
                }
                else
                {
                    throw new Exception("登录失败");
                }
            }
        }

        /// <summary>
        /// Form置顶
        /// </summary>
        /// <returns></returns>
        private static IWin32Window Top()
        {
            IWin32Window iWin32Window = new Form()
                { TopMost = true, WindowState = FormWindowState.Maximized };
            return iWin32Window;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        private static void ResetPassword()
        {
            ResetPasswordForm form = new ResetPasswordForm();
            if (form.ShowDialog(Top()) == DialogResult.OK)
            {
                string newPwd = form.NewPassword;
                string repeatPwd = form.RepeatPassword;
                if (newPwd != repeatPwd)
                {
                    throw new Exception("两次密码输入不一致。");
                }

                if (DataSetting.Password(User.Default.UserId, newPwd))
                {
                    MessageBox.Show("密码重置成功！", "重置密码", MessageBoxButtons.OK);
                }
            }
        }

        /// <summary>
        /// 登录状态有效
        /// </summary>
        /// <returns></returns>
        private static bool SessionValid()
        {
            bool state = User.Default.LoginState;
            if (state)
            {
                TimeSpan timeSpan = DateTime.Now - User.Default.LastLoginTime;
                if (timeSpan.TotalHours > 168)
                {
                    User.Default.LoginState = false;
                    User.Default.Save();
                }
            }

            return state;
        }
    }
} 