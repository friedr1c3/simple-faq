using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleFAQ.Models
{
	/// <summary>
	/// Setting.
	/// </summary>
	public class Setting
	{
		#region Properties

		/// <summary>
		/// Gets or sets the ID.
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public string Value { get; set; }

		#endregion
	}
}