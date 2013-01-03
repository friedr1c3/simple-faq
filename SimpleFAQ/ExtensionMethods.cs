using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleFAQ
{
	/// <summary>
	/// Extension methods.
	/// </summary>
	public static class ExtensionMethods
	{
		#region Public Methods

		public static bool IsNullOrEmpty(this string str)
		{
			return String.IsNullOrEmpty(str);
		}

		public static bool HasValue(this string str)
		{
			return !str.IsNullOrEmpty();
		}

		#endregion
	}
}