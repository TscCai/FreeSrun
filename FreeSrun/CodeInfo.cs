using System.Collections.Generic;

namespace FreeSrun
{
	public class CodeInfo
	{
		public static Dictionary<string, string> InfoDict { get { return infoDict; } }
		private static Dictionary<string, string> infoDict;
		static CodeInfo()
		{
			infoDict = new Dictionary<string, string>();
			infoDict.Add("user_tab_error", "认证程序未启动");
			infoDict.Add("username_error", "用户名错误");
			infoDict.Add("non_auth_error", "您无须认证，可直接上网");
			infoDict.Add("password_error", "密码错误");
			infoDict.Add("status_error", "用户已欠费，请尽快充值");
			infoDict.Add("available_error", "用户已禁用");
			infoDict.Add("ip_exist_error", "IP已存在");
			infoDict.Add("usernum_error", "用户数已达上限");
			infoDict.Add("online_num_error", "该帐号的登录人数已超过限额");
			infoDict.Add("mode_error", "客户端模式错误");
			infoDict.Add("time_policy_error", "当前时段不允许连接");
			infoDict.Add("flux_error", "您的流量已超支");
			infoDict.Add("minutes_error", "您的时长已超支");
			infoDict.Add("ip_error", "IP地址不合法");
			infoDict.Add("mac_error", "MAC地址不合法");
			infoDict.Add("logout_error", "登出失败");
			infoDict.Add("logout_ok", "已登出");
			infoDict.Add("uid_error", "uid_error");
		}
	}
}
