using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FreeSrun.Util
{

	/// <summary>
	/// 创建request请求
	/// </summary>
	public class WebRequestHelper
	{
		/// <summary>
		/// 创建request请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="ParamterList"></param>
		/// <returns></returns>
		public static string CreateRequest(string url, string method, Dictionary<string, string> ParamterList)
		{
			StringBuilder formData = new StringBuilder();
			if (ParamterList != null && ParamterList.Count > 0)
			{
				foreach (string key in ParamterList.Keys)
				{
					if (formData.ToString() == "")
					{
						formData.Append(key + "=" + System.Web.HttpUtility.UrlEncode(ParamterList[key]));
					}
					else
					{
						formData.Append("&" + key + "=" + System.Web.HttpUtility.UrlEncode(ParamterList[key]));
					}
				}
			}
			return CreateRequest(url, method, formData.ToString(), Encoding.ASCII, Encoding.ASCII);
		}

		public static string CreateRequest(string url, string method, string formData)
		{
			return CreateRequest(url, method, formData, Encoding.ASCII, Encoding.ASCII);
		}

		public static string CreateRequest(string url, string method, string formData, Encoding reqEncoding, Encoding resEncoding)
		{
			string responseText = "";
			HttpWebRequest request;
			HttpWebResponse response;

			request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = method.ToUpper();
			request.Timeout = 5000;
			// 内容类型

			request.ContentType = "application/x-www-form-urlencoded";

			// 参数经过URL编码

			byte[] payload = null;
			if (!string.IsNullOrEmpty(formData))
			{
				//将URL编码后的字符串转化为字节
				payload = reqEncoding.GetBytes(formData);
				//设置请求的 ContentLength 
				request.ContentLength = payload.Length;
			}
			//获得请 求流 POST特有
			if (request.Method == "POST")
			{
				System.IO.Stream writer = request.GetRequestStream();
				if (payload != null && payload.Length > 0)
				{
					//将请求参数写入流
					writer.Write(payload, 0, payload.Length);
				}
				// 关闭请求流
				writer.Close();
			}
			// 获得响应流
			response = (System.Net.HttpWebResponse)request.GetResponse();
			System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), resEncoding);
			responseText = reader.ReadToEnd();
			reader.Close();

			return responseText;
		}
	}
}

