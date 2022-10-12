using System;
using System.Windows.Forms;

namespace Kengic.Opns.Database.UI
{
    public partial class ResetPasswordForm : Form
    {
        public string NewPassword => textBoxNewPassword.Text;
        public string RepeatPassword => textBoxRepeatPassword.Text;

        public ResetPasswordForm()
        {
            InitializeComponent();
        }

        private void textBoxNewPassword_TextChanged(object sender, EventArgs e)
        {
            buttonChange.Enabled = false;
            if (IsNewPwdValid())
            {
                errorProvider1.SetError(textBoxNewPassword, string.Empty);
            }
            else
            {
                errorProvider1.SetError(textBoxNewPassword, "密码长度必须大于8位且小于16位");
                return;
            }

            if (IsNewPwdValid() & IsRepetPwdValid())
            {
                buttonChange.Enabled = true;
            }
        }

        private void textBoxRepeatPassword_TextChanged(object sender, EventArgs e)
        {
            buttonChange.Enabled = false;
            if (IsRepetPwdValid())
            {
                errorProvider1.SetError(textBoxRepeatPassword, string.Empty);
            }
            else
            {
                errorProvider1.SetError(textBoxRepeatPassword, "密码长度必须大于8位且小于16位");
                return;
            }
            if (IsNewPwdValid() & IsRepetPwdValid())
            {
                buttonChange.Enabled = true;
            }
        }

        private bool IsNewPwdValid()
        {
            int textLenth = textBoxNewPassword.Text.Length;
            return (textLenth > 8 & textLenth < 16);
        }

        private bool IsRepetPwdValid()
        {
            int textLenth = textBoxRepeatPassword.Text.Length;
            return (textLenth > 8 & textLenth < 16);
        }
    }
}
