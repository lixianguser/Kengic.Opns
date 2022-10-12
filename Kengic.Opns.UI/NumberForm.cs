using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Kengic.Opns.UI
{
    public partial class NumberForm : Form
    {
        public NumberForm()
        {
            InitializeComponent();
        }

        public int StartingNumber => (int)numericUpDownStarting.Value;//定义变量，起始号
        public int Increment => (int)numericUpDownIncrement.Value;//定义变量，递增
        public static DataSet DataSet { get; set; }
        public static string DataMember { get; set; }

        //加载窗体时触发事件
        private void NumberForm_Load(object sender, EventArgs e)
        {
            Size = new Size(256, 170);
            //获取数据
            dataGridView1.DataSource = DataSet;
            dataGridView1.DataMember = DataMember;
            //设置datagridview控件高度自适应数据内容高度
            dataGridView1.Height = dataGridView1.Rows.Count * 
                dataGridView1.RowTemplate.Height + dataGridView1.ColumnHeadersHeight;
        }

        //输入数字时触发事件
        private void numericUpDownStarting_Enter(object sender, EventArgs e)
        {
            numericUpDownStarting.Select(0, numericUpDownStarting.Text.Length);
        }

        private void numericUpDownIncrement_Enter(object sender, EventArgs e)
        {
            numericUpDownIncrement.Select(0, numericUpDownIncrement.Text.Length);
        }

        private void buttonFold_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = !dataGridView1.Visible;
            if (dataGridView1.Visible)
            {
                buttonFold.Text = "▾    规则";
                //设置窗体在屏幕顶部
                int x = (SystemInformation.WorkingArea.Width - Size.Width) / 2;
                StartPosition = FormStartPosition.Manual;
                Location = (Point)new Size(x,0);
            }
            else
            {
                buttonFold.Text = "▸    规则";
                //设置窗体尺寸
                Size = new Size(256, 170);
                //设置窗体在屏幕中间
                int x = (SystemInformation.WorkingArea.Width - Size.Width) / 2;
                int y = (SystemInformation.WorkingArea.Height - Size.Height) / 2;
                StartPosition = FormStartPosition.CenterScreen;
                Location = (Point)new Size(x, y);
            }
        }
    }
}
