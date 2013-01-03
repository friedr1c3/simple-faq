using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace SimpleFAQ
{
	/// <summary>
	/// App settings.
	/// </summary>
	public class AppSettings
	{
		#region Constructor

		static AppSettings()
		{
			Refresh();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// 
		/// </summary>
		public static void Refresh()
		{
			var data = Current.DB.Settings.All().ToDictionary(v => v.Name, v => v.Value);

			foreach (var property in typeof(AppSettings).GetProperties(BindingFlags.Static | BindingFlags.Public))
			{
				string overrideData;

				if (data.TryGetValue(property.Name, out overrideData))
				{
					if (property.PropertyType == typeof(bool))
					{
						bool parsed = false;
						Boolean.TryParse(overrideData, out parsed);
						property.SetValue(null, parsed, null);
					}

					else if (property.PropertyType == typeof(int))
					{
						int parsed = -1;
						if (int.TryParse(overrideData, out parsed))
						{
							property.SetValue(null, parsed, null);
						}
					}

					else if (property.PropertyType == typeof(string))
					{
						property.SetValue(null, overrideData, null);
					}

					//else if (overrideData[0] == '{' && overrideData[overrideData.Length - 1] == '}')
					//{
					//	try
					//	{
					//		property.SetValue(null, JsonConvert.DeserializeObject(overrideData, property.PropertyType), null);
					//	}

					//	catch (JsonSerializationException)
					//	{
					//		// Just in case
					//		property.SetValue(null, null, null);
					//	}
					//}
				}
			}
		}


		#endregion

		#region Properties

		public static string SiteName
		{
			get;
			set;
		}

		public static bool RegistrationEnabled
		{
			get;
			set;
		}

		#endregion
	}
}