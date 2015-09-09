using System;
using System.Collections.Generic;
using System.Text;

namespace FreeSrun
{
	public abstract class ResponseResult
	{
		public ResponseStatus Status { get; set; }
		public string Message { get; set; }
		public string ResponseText { get; set; }
	}
	public enum ResponseStatus { Success, Error, Unkonw }
}
