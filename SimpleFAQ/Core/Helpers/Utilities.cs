using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleFAQ.Core.Helpers
{
	/// <summary>
	/// Useful utilities.
	/// </summary>
	public static class Utilities
	{
		#region Public Methods

		/// <summary>
		/// Generates a random string.
		/// </summary>
		/// <returns></returns>
		public static string RandomString(int length = 10)
		{
			string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456!@#$%^&*()=~_";
			string res = string.Empty;
			var rnd = new Random();

			while (0 < length--)
			{
				res += valid[rnd.Next(valid.Length)];
			}

			return res;
		}

		#endregion
	}
}