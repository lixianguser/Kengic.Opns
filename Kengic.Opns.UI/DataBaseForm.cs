using System;
using System.Data;
using System.Windows.Forms;

namespace Kengic.Opns.UI
{
    public partial class DataBaseForm : Form
    {
        public static DataSet DataSet { get; set; }
        public static string DataMember { get; set; }

        public DataBaseForm()
        {
            InitializeComponent();
        }

        private void DataBaseForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DataSet;
            dataGridView1.DataMember = DataMember;
        }
    }
}
