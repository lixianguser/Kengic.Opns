
namespace Kengic.Opns.UI
{
    partial class AssignmentOfProDiagFBsForm
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
            this.comboProdiagFB = new System.Windows.Forms.ComboBox();
            this.labelAssigned = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboProdiagFB
            // 
            this.comboProdiagFB.FormattingEnabled = true;
            this.comboProdiagFB.Location = new System.Drawing.Point(122, 12);
            this.comboProdiagFB.Name = "comboProdiagFB";
            this.comboProdiagFB.Size = new System.Drawing.Size(121, 21);
            this.comboProdiagFB.TabIndex = 0;
            // 
            // labelAssigned
            // 
            this.labelAssigned.AutoSize = true;
            this.labelAssigned.Location = new System.Drawing.Point(6, 15);
            this.labelAssigned.Name = "labelAssigned";
            this.labelAssigned.Size = new System.Drawing.Size(110, 13);
            this.labelAssigned.TabIndex = 1;
            this.labelAssigned.Text = "指定的 ProDiag FB:";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(67, 47);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(168, 47);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // AssignmentOfProDiagFBsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 82);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelAssigned);
            this.Controls.Add(this.comboProdiagFB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssignmentOfProDiagFBsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProDiag";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AssignmentOfProDiagFBsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboProdiagFB;
        private System.Windows.Forms.Label labelAssigned;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}