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
	public partial class Frm_Main : Form
	{

		SrunService srunSvc = new SrunService();
		string notifyMsg = "";
		public Frm_Main()
		{
			InitializeComponent();
		}

		public Frm_Main(Configuration config)
			: this()
		{
			srunSvc.Config = config;
			CommonService.logger.Level = config.LogLevel;
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			string[] args = { "-u", txtUsername.Text, "-p", txtPassword.Text, "-add", txtAddress.Text };
			if (Configuration.CheckParam(args))
			{
				srunSvc.Config = Configuration.Configure(args);
				CommonService.logger.Level = srunSvc.Config.LogLevel;
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

		private void DoLogin()
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
					notifyMsg = srunSvc.GetNotifyMessage();
					ntyIcon.BalloonTipText = notifyMsg;
					ntyIcon.ShowBalloonTip(srunSvc.Config.NotifyDuration * 1000);
				}
#if !FAKE_LOGIN
			}
			else
			{
				lblHint.Text = CodeInfo.InfoDict[result.Message];
			}
			if (srunSvc.Config.LogLevel == SimpleLogger.LogLevel.Debug)
			{
				CommonService.logger.AppendLog("Login.\r\n"
					+ "Login url: {0}\r\n" + "Login port: {1}\r\n"
					+ "Server IP: {2}\r\n" + "Heartbeat port: {3} \r\n" + "Heartbeat interval: {4} ms\r\n"
					+ "Logout url: {5}\r\n" + "Log level: {6}\r\n"
					+ "Message url: {7}\r\n" + "Notify duration: {8} second(s)\r\n"
					+ "Query url: {9}\r\n" + "Timestamp offset: {10}\r\n"
					+ "Username: {11}\r\n",
					srunSvc.Config.LoginUrl, srunSvc.Config.LoginPort,
						srunSvc.Config.ServerIP, srunSvc.Config.HeartBeatPort, srunSvc.Config.HeartBeatInterval,
						srunSvc.Config.LogoutUrl, srunSvc.Config.LogLevel,
						srunSvc.Config.MessageUrl, srunSvc.Config.NotifyDuration,
						srunSvc.Config.QueryUrl, srunSvc.Config.TimestampOffset,
						srunSvc.Config.Username
					);

			}
#endif
		}

		private void DoLogout()
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
				if (srunSvc.Config.LogLevel != SimpleLogger.LogLevel.None)
				{
					CommonService.logger.AppendLog(ex);
				}
			}
		}

		private void btnLogout_Click(object sender, EventArgs e)
		{
			DoLogout();
		}

		private void FrmMain_Load(object sender, EventArgs e)
		{
			ntyIcon.Icon = this.Icon;
			if (srunSvc.Config != null && srunSvc.Config.IsConfigured)
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

		private void tsmi_Exit_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("即将退出FreeSrun并登出，是否确定？", "退出", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				DoLogout();
				Application.Exit();
			}
		}

		private void tsmi_About_Click(object sender, EventArgs e)
		{
			Frm_About frm_About = new Frm_About(notifyMsg);
			frm_About.ShowDialog();
		}


	}
}
