using System;

namespace FreeSrun
{
	public class LoginResponseResult : ResponseResult
	{
		public string Uid { get; set; }
		public long Timestamp { get; set; }
		public long TimeOffset { get; set; }
		public LoginResponseResult(ResponseStatus status, string message)
		{
			Status = status;
			Message = message;
		}
		public LoginResponseResult(string responseText)
		{
			ResponseText = responseText;
			// If error: message@timestamp
			if (responseText.Contains(","))
			{
				Status = ResponseStatus.Success;
				Uid = responseText.Split(',')[0];
				Timestamp = Convert.ToInt64(responseText.Split(',')[4]);
			}
			else
			{
				Status = ResponseStatus.Error;
				if (responseText.Contains("@"))
				{
					Message = responseText.Split('@')[0];
					Timestamp = Convert.ToInt64(responseText.Split('@')[1]);
				}
				else
				{
					Message = CodeInfo.InfoDict[responseText];
				}
			}

		}

		public override string ToString()
		{
			string result = "";
			result += "[" + Timestamp + "]" + "Login: " + Status + "\nMessage: " + Message;
			return result;
		}
	}
}
