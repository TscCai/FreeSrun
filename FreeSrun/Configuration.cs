using System;
using System.Collections.Generic;
using System.Linq;
using FreeSrun.Util;

namespace FreeSrun
{

	public class Configuration
	{
		public string LoginUrl { get; set; }
		public string LogoutUrl { get; set; }
		public string QueryUrl { get; set; }
		public string MessageUrl { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string ServerIP { get; set; }
		public int LoginPort { get; set; }
		public int HeartBeatPort { get; set; }
		/// <summary>
		/// Heartbeat interval to trigger the internal timer, in millisecond.
		/// </summary>
		public double HeartBeatInterval { get; set; }
		public long TimestampOffset { get; set; }
		public int NotifyDuration { get; set; }
		public bool IsConfigured { get; set; }
		public SimpleLogger.LogLevel LogLevel { get; set; }


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
				info += "[-hi] Heartbeat packet interval, in minute, decimal support, 2 defualt\n";
				info += "[-nl] Notify balloon duration, in second, 3 default\n";
				info += "[-to] Timestamp offset between local time and server time\n";
				info += "[-l] Set the log level\n";
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
		///     [-nl] Notify balloon duration, in second, 3 default
		///     [-to] Timestamp offset between local time and server time";
		///     [-l] Set the log level
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
			result.LogLevel = SimpleLogger.LogLevel.Warning;
			result.HeartBeatInterval = 2 * 60 * 1000;

			// Configure Optional arguments
			if (list.ContainsKey("-lp"))
			{
				result.LoginPort = Convert.ToInt32(list["-lp"]);
			}
			if (list.ContainsKey("-hp"))
			{
				result.HeartBeatPort = Convert.ToInt32(list["-hp"]);
			}
			if (list.ContainsKey("-hi"))
			{
				double val = Convert.ToDouble(list["-hi"]);
				if (result.HeartBeatInterval > 0)
				{
					result.HeartBeatInterval = val*1000*60;
				}
			}
			if (list.ContainsKey("-nl"))
			{
				result.NotifyDuration = Convert.ToInt32(list["-nl"]);
				if (result.NotifyDuration < 0)
				{
					result.NotifyDuration = 0;
				}
			}
			if (list.ContainsKey("-to"))
			{
				result.TimestampOffset = Convert.ToInt64(list["-to"]);
			}
			if (list.ContainsKey("-l"))
			{
				string logLevel = list["-l"];
				switch (logLevel)
				{
					// Debug mode has the most detailed log.
					case "debug":
					case "d":
						result.LogLevel = SimpleLogger.LogLevel.Debug;
						break;
					// Warning mode only log for exceptions.
					case "warning":
					case "w":
						result.LogLevel = SimpleLogger.LogLevel.Warning;
						break;
					// None mode log nothing.
					case "none":
					case "no":
					case "n":
						result.LogLevel = SimpleLogger.LogLevel.None;
						break;
					default:
						result.LogLevel = SimpleLogger.LogLevel.Warning;
						break;
				}
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
