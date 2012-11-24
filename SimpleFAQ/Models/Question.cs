using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleFAQ.Models
{
	/// <summary>
	/// Question.
	/// </summary>
	public class Question
	{
		#region Properties

		/// <summary>
		/// Gets or sets the ID.
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the answer.
		/// </summary>
		public string Answer { get; set; }

		/// <summary>
		/// Gets or sets the owner user ID.
		/// </summary>
		public int OwnerUserID { get; set; }

		/// <summary>
		/// Gets or sets the time.
		/// </summary>
		public DateTime Time { get; set; }

		/// <summary>
		/// Gets or sets the view count.
		/// </summary>
		public int View { get; set; }

		#endregion
	}
}