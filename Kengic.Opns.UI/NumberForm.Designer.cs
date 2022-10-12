
namespace Kengic.Opns.UI
{
    partial class NumberForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.numericUpDownStarting = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownIncrement = new System.Windows.Forms.NumericUpDown();
            this.labelStartingNumber = new System.Windows.Forms.Label();
            this.labelIncrement = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonFold = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStarting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIncrement)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownStarting
            // 
            this.numericUpDownStarting.BackColor = System.Drawing.Color.White;
            this.numericUpDownStarting.Location = new System.Drawing.Point(108, 0);
            this.numericUpDownStarting.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownStarting.Maximum = new decimal(new int[] {
            59999,
            0,
            0,
            0});
            this.numericUpDownStarting.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStarting.Name = "numericUpDownStarting";
            this.numericUpDownStarting.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDownStarting.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownStarting.TabIndex = 0;
            this.numericUpDownStarting.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStarting.Enter += new System.EventHandler(this.numericUpDownStarting_Enter);
            // 
            // numericUpDownIncrement
            // 
            this.numericUpDownIncrement.Location = new System.Drawing.Point(108, 0);
            this.numericUpDownIncrement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownIncrement.Name = "numericUpDownIncrement";
            this.numericUpDownIncrement.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownIncrement.TabIndex = 1;
            this.numericUpDownIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownIncrement.Enter += new System.EventHandler(this.numericUpDownIncrement_Enter);
            // 
            // labelStartingNumber
            // 
            this.labelStartingNumber.AutoSize = true;
            this.labelStartingNumber.Location = new System.Drawing.Point(41, 4);
            this.labelStartingNumber.Name = "labelStartingNumber";
            this.labelStartingNumber.Size = new System.Drawing.Size(46, 13);
            this.labelStartingNumber.TabIndex = 2;
            this.labelStartingNumber.Text = "起始值:";
            // 
            // labelIncrement
            // 
            this.labelIncrement.AutoSize = true;
            this.labelIncrement.Location = new System.Drawing.Point(53, 3);
            this.labelIncrement.Margin = new System.Windows.Forms.Padding(3);
            this.labelIncrement.Name = "labelIncrement";
            this.labelIncrement.Size = new System.Drawing.Size(34, 13);
            this.labelIncrement.TabIndex = 3;
            this.labelIncrement.Text = "增量:";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(56, 75);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(153, 75);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonFold
            // 
            this.buttonFold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFold.FlatAppearance.BorderSize = 0;
            this.buttonFold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFold.Location = new System.Drawing.Point(0, 104);
            this.buttonFold.Name = "buttonFold";
            this.buttonFold.Size = new System.Drawing.Size(69, 19);
            this.buttonFold.TabIndex = 6;
            this.buttonFold.Text = "▸    规则";
            this.buttonFold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFold.UseVisualStyleBackColor = true;
            this.buttonFold.Click += new System.EventHandler(this.buttonFold_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 10);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.numericUpDownStarting);
            this.panel2.Controls.Add(this.labelStartingNumber);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 20);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 10);
            this.panel3.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.labelIncrement);
            this.panel4.Controls.Add(this.numericUpDownIncrement);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 40);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(240, 20);
            this.panel4.TabIndex = 11;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 60);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(240, 38);
            this.panel5.TabIndex = 12;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 98);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(240, 33);
            this.panel6.TabIndex = 13;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.GridColor = System.Drawing.Color.Black;
            this.dataGridView1.Location = new System.Drawing.Point(0, 131);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Size = new System.Drawing.Size(240, 21);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.Visible = false;
            // 
            // NumberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(240, 156);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonFold);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NumberForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重新编号";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.NumberForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStarting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIncrement)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownStarting;
        private System.Windows.Forms.NumericUpDown numericUpDownIncrement;
        private System.Windows.Forms.Label labelStartingNumber;
        private System.Windows.Forms.Label labelIncrement;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonFold;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}