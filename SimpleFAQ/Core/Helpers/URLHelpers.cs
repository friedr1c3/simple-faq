using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SimpleFAQ.Core.Helpers
{
	public static class URLHelpers
	{
		/// <summary>
		/// Creates a URL fragment.
		/// </summary>
		/// <param name="value">Fragement to create from</param>
		/// <param name="maxLength">Maximum length of the URL fragement</param>
		/// <returns>URL fragment (this-is-my-new-url)</returns>
		public static string ToURLFragment(string value, int maxLength = 50)
		{
			if (String.IsNullOrEmpty(value))
			{
				return String.Empty;
			}

			value = value.ToLowerInvariant();
			value = Regex.Replace(value, @"[^a-z-0-9 ]+", "");
			value = Regex.Replace(value, @" ", "-");
			value = Regex.Replace(value, @"-+", "-");
			value = Regex.Replace(value, @"^-+|-+$", "");

			if (value.Length > maxLength)
			{
				value = value.Substring(0, maxLength);
			}

			return value;
		}
	}
}