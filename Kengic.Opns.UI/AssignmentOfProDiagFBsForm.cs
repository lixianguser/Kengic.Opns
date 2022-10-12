using System;
using System.Collections;
using System.Windows.Forms;

namespace Kengic.Opns.UI
{
    public partial class AssignmentOfProDiagFBsForm : Form
    {
        public AssignmentOfProDiagFBsForm()
        {
            InitializeComponent();
        }
        public string SelectedValueText => (string)comboProdiagFB.SelectedValue;//定义变量，起始号
        public static ArrayList _arrayList;//定义数组

        //加载窗体时触发事件
        private void AssignmentOfProDiagFBsForm_Load(object sender, EventArgs e)
        {
            comboProdiagFB.DataSource = _arrayList;
            comboProdiagFB.DisplayMember = "DisplayMember";
            comboProdiagFB.ValueMember = "ValueMember";
        }
    }
}
