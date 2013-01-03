using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleFAQ.ViewModels
{
	using Models;

	/// <summary>
	/// View question view model.
	/// </summary>
	public class ViewQuestion
	{
		#region Properties

		/// <summary>
		/// Gets or sets the question.
		/// </summary>
		public Question Question { get; set; }

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		public User User { get; set; }

		#endregion
	}
}