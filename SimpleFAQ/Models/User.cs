using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleFAQ.Models
{
	/// <summary>
	/// User.
	/// </summary>
	public class User
	{
		#region Properties

		/// <summary>
		/// Gets or sets the ID.
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the is admin.
		/// </summary>
		public bool IsAdmin { get; set; }

		/// <summary>
		/// Gets or sets the registered date.
		/// </summary>
		public DateTime Registered { get; set; }

		#endregion
	}
}