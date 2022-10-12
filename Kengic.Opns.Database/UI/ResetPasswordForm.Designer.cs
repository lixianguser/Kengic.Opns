
namespace Kengic.Opns.Database.UI
{
    partial class ResetPasswordForm
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonChange = new System.Windows.Forms.Button();
            this.textBoxNewPassword = new System.Windows.Forms.TextBox();
            this.labelNewPassword = new System.Windows.Forms.Label();
            this.textBoxRepeatPassword = new System.Windows.Forms.TextBox();
            this.labelRepeatPassword = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(237, 85);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonChange
            // 
            this.buttonChange.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonChange.Location = new System.Drawing.Point(144, 85);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(75, 23);
            this.buttonChange.TabIndex = 6;
            this.buttonChange.Text = "重置";
            this.buttonChange.UseVisualStyleBackColor = true;
            // 
            // textBoxNewPassword
            // 
            this.textBoxNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxNewPassword.Location = new System.Drawing.Point(144, 15);
            this.textBoxNewPassword.MaxLength = 30;
            this.textBoxNewPassword.Name = "textBoxNewPassword";
            this.textBoxNewPassword.PasswordChar = '●';
            this.textBoxNewPassword.Size = new System.Drawing.Size(154, 23);
            this.textBoxNewPassword.TabIndex = 4;
            this.textBoxNewPassword.TextChanged += new System.EventHandler(this.textBoxNewPassword_TextChanged);
            // 
            // labelNewPassword
            // 
            this.labelNewPassword.AutoSize = true;
            this.labelNewPassword.Location = new System.Drawing.Point(50, 20);
            this.labelNewPassword.Name = "labelNewPassword";
            this.labelNewPassword.Size = new System.Drawing.Size(49, 13);
            this.labelNewPassword.TabIndex = 1;
            this.labelNewPassword.Text = "新密码: ";
            // 
            // textBoxRepeatPassword
            // 
            this.textBoxRepeatPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxRepeatPassword.Location = new System.Drawing.Point(144, 49);
            this.textBoxRepeatPassword.MaxLength = 30;
            this.textBoxRepeatPassword.Name = "textBoxRepeatPassword";
            this.textBoxRepeatPassword.PasswordChar = '●';
            this.textBoxRepeatPassword.Size = new System.Drawing.Size(154, 23);
            this.textBoxRepeatPassword.TabIndex = 5;
            this.textBoxRepeatPassword.TextChanged += new System.EventHandler(this.textBoxRepeatPassword_TextChanged);
            // 
            // labelRepeatPassword
            // 
            this.labelRepeatPassword.AutoSize = true;
            this.labelRepeatPassword.Location = new System.Drawing.Point(26, 54);
            this.labelRepeatPassword.Name = "labelRepeatPassword";
            this.labelRepeatPassword.Size = new System.Drawing.Size(73, 13);
            this.labelRepeatPassword.TabIndex = 2;
            this.labelRepeatPassword.Text = "重复新密码: ";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // ResetPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 120);
            this.Controls.Add(this.labelRepeatPassword);
            this.Controls.Add(this.textBoxRepeatPassword);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.textBoxNewPassword);
            this.Controls.Add(this.labelNewPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResetPasswordForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重置密码";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.TextBox textBoxNewPassword;
        private System.Windows.Forms.Label labelNewPassword;
        private System.Windows.Forms.TextBox textBoxRepeatPassword;
        private System.Windows.Forms.Label labelRepeatPassword;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}