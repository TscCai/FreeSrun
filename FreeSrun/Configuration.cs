using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeSrun
{
    public class Configuration
    {
        public string LoginUrl { get; set; }
        public string LogoutUrl { get; set; }
        public string QueryUrl { get; set; }
        public string MessageUrl { get; set; }
        public string Username{get;set;}
        public string Password { get; set; }
        public string ServerIP { get; set; }
        public int LoginPort { get; set; }
        public int HeartBeatPort { get; set; }
        public long TimestampOffset { get; set; }
        public int NotifyDuration { get; set; }
        public bool IsConfigured { get; set; }

        public static string HelpInfo
        {
            get
            {
                string info = "";
                info += "All parameters list:\n";
                info += "-u Username\n";
                info += "-p Password\n";
                info += "-add Server IP address\n";
                info += "[-lp] Login port, 3333 default\n";
                info += "[-hp] Heartbeat packets port, 3335 default\n";
                info += "[-nl] Notify balloon last time, in second, 3 default\n";
                info += "[-to] Timestamp offset between local time and server time\n";
                info += "[-help][-h][-?] Show the help info";
                return info;
            }
        }

        /// <summary>
        /// Leagal Param:
        ///     -u Username
        ///     -p Password
        ///     -add Server IP address"
        ///     [-lp] Login port, 3333 default
        ///     [-hp] Heartbeat packets port, 3335 default
        ///     [-nl] Notify balloon last time, in second, 3 default
        ///     [-to] Timestamp offset between local time and server time";
        ///     [-h/-?/-help] Show the help info
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool CheckParam(string[] args)
        {
            bool result = true;
            if (args.Length % 2 == 1 || args.Length == 0)
            {
                result = false;
            }
            if (!(args.Contains("-u") && args.Contains("-p") && args.Contains("-add")))
            {
                result = false;
            }
            return result;
        }

        public static Configuration Configure(string[] args)
        {
            Configuration result = new Configuration();
            Dictionary<string, string> list = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i += 2)
            {
                list.Add(args[i], args[i + 1]);
            }
            result.Username = list["-u"];
            result.Password = list["-p"];
            result.ServerIP = list["-add"];

            // Optional arguments default value
            result.HeartBeatPort = 3335;
            result.LoginPort = 3333;
            result.NotifyDuration = 3;
            result.TimestampOffset = 0;

            // Configure Optional arguments
            if (list.Keys.Contains("-lp"))
            {
                result.LoginPort = Convert.ToInt32(list["-lp"]);
            }
            if (list.Keys.Contains("-hp"))
            {
                result.HeartBeatPort = Convert.ToInt32(list["-hp"]);
            }
            if (list.Keys.Contains("-nl"))
            {
                result.NotifyDuration = Convert.ToInt32(list["-nl"]);
                if (result.NotifyDuration < 0)
                {
                    result.NotifyDuration= 0;
                }
            }
            if (list.Keys.Contains("-to"))
            {
                result.TimestampOffset = Convert.ToInt64(list["-to"]);
            }


            result.LoginUrl = "http://" + result.ServerIP + ":" + result.LoginPort + "/cgi-bin/do_login";
            result.LogoutUrl = "http://" + result.ServerIP + ":" + result.LoginPort + "/cgi-bin/do_logout";
            result.QueryUrl = "http://" + result.ServerIP + ":" + result.LoginPort + "/cgi-bin/keeplive";
            result.MessageUrl = "http://" + result.ServerIP + ":" + result.LoginPort + "/get_msg.php";
            result.IsConfigured = true;
            return result;
        }
    }
}
