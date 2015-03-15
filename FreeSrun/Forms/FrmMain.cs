#if DEBUG
#define FAKE_LOGIN
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using FreeSrun.Util;
using System.Runtime.InteropServices;

namespace FreeSrun.Forms
{
    public partial class FrmMain : Form
    {

        SrunService srunSvc = new SrunService();

        public FrmMain()
        {
            InitializeComponent();
            this.Text = "FreeSrun " + this.ProductVersion;
        }

        public FrmMain(Configuration config):this()
        {
            srunSvc.Config = config;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string[] args = { "-u", txtUsername.Text, "-p", txtPassword.Text, "-add", txtAddress.Text };
            if (Configuration.CheckParam(args))
            {
                srunSvc.Config = Configuration.Configure(args);
                try
                {
                    DoLogin();
                }
                catch (Exception ex)
                {
                    CommonService.logger.AppendLog(ex);
                }
            }
            else
            {
                MessageBox.Show("运行参数错误");
            }
        }

        public void DoLogin()
        {
#if !FAKE_LOGIN
            LoginResponseResult result = srunSvc.Login();
            if (result.Status == ResponseStatus.Success)
            {
                string uid = result.Uid;
                byte[] packet = CommonService.BuildHeartBeatPacket(uid);
                srunSvc.Status = new StatusInfo(uid, packet);

                srunSvc.HeartBeat();
#else
            LoginResponseResult result = new LoginResponseResult(ResponseStatus.Success, "");
#endif

                srunSvc.Status.IsLogin = true;
                // Update UI
                lblHint.Text = "已登录\n\n时间戳偏差为：" + result.TimeOffset;
#if FAKE_LOGIN
            lblHint.Text += "\t FAKE_LOGIN";
#endif
                txtAddress.Enabled = false;
                txtPassword.Enabled = false;
                txtUsername.Enabled = false;
                btnLogin.Enabled = false;

                // Hide Form
                this.WindowState = FormWindowState.Minimized;

                // Get notify message
                if (srunSvc.Config.NotifyDuration > 0)
                {
                    string msg = srunSvc.GetNotifyMessage();
                    ntyIcon.BalloonTipText = msg;
                    ntyIcon.ShowBalloonTip(srunSvc.Config.NotifyDuration * 1000);
                }
#if !FAKE_LOGIN
            }
            else
            {
                lblHint.Text = CodeInfo.InfoDict[result.Message];
            }
#endif
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                LogoutResponseResult result = srunSvc.Logout();
                if (result.Status == ResponseStatus.Success)
                {
                    srunSvc.Status.IsLogin = false;
                }
                lblHint.Text = result.Message;
                txtAddress.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                btnLogin.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonService.logger.AppendLog(ex);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ntyIcon.Icon = this.Icon;
            if (srunSvc.Config !=null &&srunSvc.Config.IsConfigured)
            {
                txtAddress.Text = srunSvc.Config.ServerIP;
                txtUsername.Text = srunSvc.Config.Username;
                txtPassword.Text = srunSvc.Config.Password;
                try
                {
                    DoLogin();
                }
                catch (Exception ex)
                {
                    CommonService.logger.AppendLog(ex);
                }
            }
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                // Change FormBorderStyle to FiexdToolWindow, otherwise 
                // it won't guarantee to hide from Alt+Tab dialog.
                this.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        private void ntyIcon_DoubleClick(object sender, EventArgs e)
        {
            // Back to FixedSingle for good looking
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
            this.Activate();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
#if RELEASE
            if (srunSvc.Status != null && srunSvc.Status.IsLogin)
            {
                LogoutResponseResult result;
                try
                {
                    result = srunSvc.Logout();
                    if (result.Status == ResponseStatus.Error)
                    {
                        MessageBox.Show(CodeInfo.InfoDict[result.Message]);
                    }
                }
                catch (Exception ex)
                {
                    CommonService.logger.AppendLog(ex);
                }
                finally
                {
                    CommonService.logger.Close();
                }
            }
#endif
        }


    }
}
