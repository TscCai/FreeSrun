namespace FreeSrun.Forms
{
    partial class Frm_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
			this.lblAddress = new System.Windows.Forms.Label();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.lblUsername = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btnLogin = new System.Windows.Forms.Button();
			this.btnLogout = new System.Windows.Forms.Button();
			this.ntyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.cms_Tray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmi_About = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmi_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.lblHint = new System.Windows.Forms.Label();
			this.grb_Info = new System.Windows.Forms.GroupBox();
			this.cms_Tray.SuspendLayout();
			this.grb_Info.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblAddress
			// 
			this.lblAddress.AutoSize = true;
			this.lblAddress.Location = new System.Drawing.Point(12, 17);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(65, 12);
			this.lblAddress.TabIndex = 0;
			this.lblAddress.Text = "认证服务器";
			// 
			// txtAddress
			// 
			this.txtAddress.Location = new System.Drawing.Point(83, 14);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(117, 21);
			this.txtAddress.TabIndex = 1;
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new System.Drawing.Point(36, 51);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(41, 12);
			this.lblUsername.TabIndex = 2;
			this.lblUsername.Text = "用户名";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(83, 48);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(117, 21);
			this.txtUsername.TabIndex = 2;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(48, 86);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(29, 12);
			this.lblPassword.TabIndex = 2;
			this.lblPassword.Text = "密码";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(83, 83);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(117, 21);
			this.txtPassword.TabIndex = 3;
			this.txtPassword.UseSystemPasswordChar = true;
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(12, 174);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(75, 23);
			this.btnLogin.TabIndex = 4;
			this.btnLogin.Text = "登录";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// btnLogout
			// 
			this.btnLogout.Location = new System.Drawing.Point(125, 174);
			this.btnLogout.Name = "btnLogout";
			this.btnLogout.Size = new System.Drawing.Size(75, 23);
			this.btnLogout.TabIndex = 5;
			this.btnLogout.Text = "登出";
			this.btnLogout.UseVisualStyleBackColor = true;
			this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
			// 
			// ntyIcon
			// 
			this.ntyIcon.ContextMenuStrip = this.cms_Tray;
			this.ntyIcon.Text = "FreeSrun";
			this.ntyIcon.Visible = true;
			this.ntyIcon.DoubleClick += new System.EventHandler(this.ntyIcon_DoubleClick);
			// 
			// cms_Tray
			// 
			this.cms_Tray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_About,
            this.toolStripMenuItem1,
            this.tsmi_Exit});
			this.cms_Tray.Name = "cms_Tray";
			this.cms_Tray.Size = new System.Drawing.Size(101, 54);
			// 
			// tsmi_About
			// 
			this.tsmi_About.Name = "tsmi_About";
			this.tsmi_About.Size = new System.Drawing.Size(100, 22);
			this.tsmi_About.Text = "关于";
			this.tsmi_About.Click += new System.EventHandler(this.tsmi_About_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(97, 6);
			// 
			// tsmi_Exit
			// 
			this.tsmi_Exit.Name = "tsmi_Exit";
			this.tsmi_Exit.Size = new System.Drawing.Size(100, 22);
			this.tsmi_Exit.Text = "退出";
			this.tsmi_Exit.Click += new System.EventHandler(this.tsmi_Exit_Click);
			// 
			// lblHint
			// 
			this.lblHint.AutoSize = true;
			this.lblHint.Location = new System.Drawing.Point(9, 16);
			this.lblHint.Name = "lblHint";
			this.lblHint.Size = new System.Drawing.Size(0, 12);
			this.lblHint.TabIndex = 6;
			// 
			// grb_Info
			// 
			this.grb_Info.Controls.Add(this.lblHint);
			this.grb_Info.Location = new System.Drawing.Point(12, 110);
			this.grb_Info.Name = "grb_Info";
			this.grb_Info.Size = new System.Drawing.Size(188, 58);
			this.grb_Info.TabIndex = 7;
			this.grb_Info.TabStop = false;
			// 
			// Frm_Main
			// 
			this.AcceptButton = this.btnLogin;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(213, 203);
			this.Controls.Add(this.btnLogout);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.lblUsername);
			this.Controls.Add(this.txtAddress);
			this.Controls.Add(this.lblAddress);
			this.Controls.Add(this.grb_Info);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			
			this.MaximizeBox = false;
			this.Name = "Frm_Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FreeSrun";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
			this.cms_Tray.ResumeLayout(false);
			this.grb_Info.ResumeLayout(false);
			this.grb_Info.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.NotifyIcon ntyIcon;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.GroupBox grb_Info;
		private System.Windows.Forms.ContextMenuStrip cms_Tray;
		private System.Windows.Forms.ToolStripMenuItem tsmi_About;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmi_Exit;
    }
}

