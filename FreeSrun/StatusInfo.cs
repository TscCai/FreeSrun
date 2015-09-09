using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeSrun
{
	public class StatusInfo
	{
		public string Uid { get; private set; }
		public bool IsLogin { get; set; }
		public byte[] HeartBeatPacket { get; private set; }

		public StatusInfo(string uid, byte[] packet)
		{
			Uid = uid;
			HeartBeatPacket = packet;
		}

	}
}
