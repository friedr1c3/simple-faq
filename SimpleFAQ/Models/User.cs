using FluentValidation.Attributes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleFAQ.Models
{
	using Validators;

	/// <summary>
	/// User.
	/// </summary>
	[Validator(typeof(UserValidator))]
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

		/// <summary>
		/// Gets or sets whether or not the current user is anonymous.
		/// </summary>
		public bool IsAnonymous { get; set; }

		/// <summary>
		/// Gets or sets the XSFR form value.
		/// </summary>
		public string XSRFFormValue { get; set; }

		#endregion
	}
}