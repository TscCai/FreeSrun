using FreeSrun.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace FreeSrun
{
	public class CommonService
	{

		public static SimpleLogger logger = new SimpleLogger();

		/// <summary>
		/// Build the login form body
		/// </summary>
		/// <param name="username"></param>
		/// <param name="encrptPassword"></param>
		/// <returns></returns>
		public static Dictionary<string, string> BuildForm(string username, string encrptPassword)
		{
			Dictionary<string, string> paramList = new Dictionary<string, string>();
			paramList.Add("username", username);
			paramList.Add("password", encrptPassword);
			paramList.Add("drop", "0"); // 是否访问免费资源，0否；1是
			paramList.Add("type", "2"); // 登录方式，1为WEB，2为客户端
			paramList.Add("n", "8");    // 客户端版本，8为WIN32
			paramList.Add("mac", ""); //mac地址，未绑定
			return paramList;
		}



		/// <summary>
		/// Build the heartbeat packet
		/// </summary>
		/// <param name="uid"></param>
		/// <returns></returns>
		public static byte[] BuildHeartBeatPacket(string uid)
		{
			// uid is a string(more than 8 bytes) stand for a Int64(8 bytes)
			// Convert the uid(Int64 format) to byte array, that's the heartbeat packet.
			// Attention: Windows/ Linux is a Little Endian system.
			byte[] result = BitConverter.GetBytes(Convert.ToInt64(uid));
			if (!BitConverter.IsLittleEndian)
			{
				Array.Reverse(result);
			}
			return result;
		}



		/// <summary>
		/// Send a UDP packet
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="port"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static byte[] SendUdp(string ip, int port, byte[] data)
		{
			IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
			UdpClient client = new UdpClient();

			client.Send(data, data.Length, ipep);
			client.Client.ReceiveTimeout = 3 * 60 * 1000;
			byte[] response = null;
			response = client.Receive(ref ipep);
			client.Close();
			return response;
		}



		/// <summary>
		/// Build timestamp without offset. It's the total seconds from 1970-1-1 to now
		/// </summary>
		/// <returns></returns>
		public static long BuildTimestamp()
		{
			TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
			long result = Convert.ToInt64(ts.TotalSeconds);
			return result;
		}

		/// <summary>
		/// Build timestamp with the given offset
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static long BuildTimestamp(long offset)
		{
			return BuildTimestamp() + offset;
		}

	}
}
