using FreeSrun.Util;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using System.Web;

namespace FreeSrun
{
    public class SrunService
    {
        public Configuration Config { get; set; }
        public StatusInfo Status { get; set; }
        
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginResponseResult Login(string url, string username, string password)
        {
            PasswordEncryptor pe = new PasswordEncryptor();
            long ts = CommonService.BuildTimestamp(Config.TimestampOffset);
            string enc_pwd = pe.Encrypt(password, ts);
            Dictionary<string, string> paramList = CommonService.BuildForm(username, enc_pwd);
            string attempt = WebRequestHelper.CreateRequest(url, "POST", paramList);

            LoginResponseResult result = new LoginResponseResult(attempt);
            // There are several kinds of error, but only "password_error" should be retried with the responsed timestamp.
            if (result.Status == ResponseStatus.Error && result.Message == "password_error")
            {
                long correctTime = result.Timestamp;
                enc_pwd = pe.Encrypt(password, Convert.ToInt64(correctTime));
                paramList["password"] = enc_pwd;
                string response = WebRequestHelper.CreateRequest(url, "POST", paramList);
                result = new LoginResponseResult(response);
                result.Timestamp = correctTime;
                result.TimeOffset = result.Timestamp - ts;
            }
            return result;
        }

        public LoginResponseResult Login()
        {
            return Login(Config.LoginUrl, Config.Username, Config.Password);
        }

        /// <summary>
        /// Initiate and start the timer
        /// </summary>
        public void HeartBeat()
        {
            Timer keep = new Timer(3 * 60 * 1000);
            keep.AutoReset = true;
            keep.Elapsed += new ElapsedEventHandler(Keep_Elapsed);
            keep.Enabled = true;
        }

        /// <summary>
        /// Send UDP when timer is tick
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void Keep_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                byte[] data = CommonService.SendUdp(Config.ServerIP, Config.HeartBeatPort, Status.HeartBeatPacket);
            }
            catch (SocketException ex)
            {
                string result = WebRequestHelper.CreateRequest(Config.QueryUrl, "GET", "");
                if (result == "error")
                {
                    CommonService.logger.AppendLog(ex);
                }
            }
        }


        /// <summary>
        /// Post uid to logout
        /// </summary>
        /// <param name="url">Logout URL</param>
        /// <param name="uid">UID, it will be recieved after login</param>
        /// <returns>logout_ok / logout_error</returns>
        public LogoutResponseResult Logout(string url, string uid)
        {
            Dictionary<string, string> form = new Dictionary<string, string>();
            form.Add("uid", uid);
            string responseText = WebRequestHelper.CreateRequest(Config.LogoutUrl, "POST", form);
            LogoutResponseResult result;
            try
            {
                result = new LogoutResponseResult(responseText);
            }
            catch (Exception ex)
            {
                result = new LogoutResponseResult(ResponseStatus.Error, ex.Message);
                result.ResponseText = responseText;
            }
            return result;
        }

        public LogoutResponseResult Logout()
        {
            return Logout(Config.LogoutUrl,Status.Uid);
        }

        /// <summary>
        /// Fetch notify messages form server
        /// </summary>
        /// <returns></returns>
        public string GetNotifyMessage()
        {
            string result = "";
            result = WebRequestHelper.CreateRequest(Config.MessageUrl, "GET", "", Encoding.ASCII, Encoding.GetEncoding("GB2312"));
            result = HttpUtility.HtmlDecode(result);
            return result;
        }

    }
}
