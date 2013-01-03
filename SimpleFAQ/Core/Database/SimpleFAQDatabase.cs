using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleFAQ.Core.Database
{
	using Models;

	/// <summary>
	/// Simple FAQ database.
	/// </summary>
	public class SimpleFAQDatabase : Dapper.Database<SimpleFAQDatabase>
	{
		#region Properties

		/// <summary>
		/// Gets or sets the questions table.
		/// </summary>
		public Table<Question> Questions { get; set; }

		/// <summary>
		/// Gets or sets the users table.
		/// </summary>
		public Table<User> Users { get; set; }

		/// <summary>
		/// Gets or sets the settings table.
		/// </summary>
		public Table<Setting> Settings { get; set; }

		#endregion
	}
}