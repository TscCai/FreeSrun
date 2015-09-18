using System;
using FreeSrun;
using FreeSrun.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace FreeSrun.Test
{
	[TestClass]
	public class UnitTest1
	{
		static byte[] heartBeatResponse = { 
			0x9e, 0x08, 0x00, 0x00, 0xfe, 0x41, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x3c, 0x00, 0x00, 0x00, 
			0x89, 0x95, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x51, 0xd5, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
			0x00, 0x00, 0x20, 0x41, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00
		};

		[TestMethod]
		public void TestMethod1()
		{
			TimeSpan ts1 = DateTime.Now - new DateTime(1970, 1, 1);
			Console.WriteLine("Total Seconds: " + ts1.TotalSeconds);

			TimeSpan ts2 = DateTime.UtcNow - new DateTime(1970, 1, 1);
			Console.WriteLine("Total Seconds(UTC): " + ts2.TotalSeconds);
		}

		[TestMethod]
		public void BuildHeartBeatPacket_Test()
		{
			byte[] result = CommonService.BuildHeartBeatPacket("10153302696068");
			Console.WriteLine(BitConverter.ToString(result));
		}

		[TestMethod]
		public void PasswordEncrpt_Test()
		{
			PasswordEncryptor pe = new PasswordEncryptor();
			long time = Convert.ToInt64((new DateTime(2014, 8, 20) - new DateTime(1970, 1, 1)).TotalSeconds);
			string en_pwd = pe.Encrypt("cjr0912", time);
			Console.WriteLine(en_pwd);
		}

		[TestMethod]
		public void Get3AIP()
		{
			string result = WebRequestHelper.CreateRequest("http://freesrun.codeplex.com", "GET", "");
			//bool result = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

			PickupAdd(result);
		}

		[TestMethod]
		public void PickupAdd(string result)
		{
			

			Regex reg1 = new System.Text.RegularExpressions.Regex(@"http://([\s\S]*)/muxjp.php");
			Match m1 = reg1.Match(result);

			if (m1.Success)
			{
				Console.Write("Success: " + m1.Groups[0].Value);
				Console.Write("IP: " + m1.Groups[1].Value);
			}
			else
			{
				Console.Write("Failed!");
			}
		}

	}
}
