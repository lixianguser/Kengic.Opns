using System;
using System.Windows.Forms;

namespace Kengic.Opns.Database.UI
{
    public partial class LoginForm : Form
    {
        public string UserId => textBoxAccount.Text;
        public string Password => textBoxPassword.Text;

        public LoginForm()
        {
            InitializeComponent();
        }
    }
}