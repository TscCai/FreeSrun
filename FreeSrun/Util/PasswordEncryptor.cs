using System;
using System.Linq;
using System.Text;

namespace FreeSrun.Util
{
	public class PasswordEncryptor
	{
		public PasswordEncryptor() { }

		public string Encrypt(string password, long time)
		{
			char[] key = Math.Floor(time / 60.0).ToString().Reverse().ToArray();

			return Confusion(password, key);
		}

		private string Confusion(string password, char[] key)
		{
			if (password.Length > 16)
			{
				password = password.Substring(0, 16);
			}
			int len = password.Length;
			StringBuilder result = new StringBuilder(2, len * 2);
			for (int i = 0; i < len; i++)
			{
				int _pass = password[i];
				int _key = key[i % key.Length];
				_key = _key ^ _pass;
				result.Append(Diffusion(_key, i % 2 == 1));
			}
			return result.ToString();
		}

		private char[] Diffusion(int num, bool isReversed)
		{
			char[] result = new char[2];

			int low = num & 0x0f;
			int high = num >> 4;
			high = high & 0x0f;

			if (!isReversed)
			{
				result[0] = (char)(low + 0x36);
				result[1] = (char)(high + 0x63);
			}
			else
			{
				result[0] = (char)(high + 0x63);
				result[1] = (char)(low + 0x36);
			}
			return result;
		}

	}
}
