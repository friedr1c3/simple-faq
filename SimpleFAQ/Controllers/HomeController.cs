using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFAQ.Controllers
{
	using Models;

	/// <summary>
	/// Home controller.
	/// </summary>
    public class HomeController : BaseController
	{
		#region Index

		/// <summary>
		/// GET: /
		/// GET: /home/
		/// GET: /home/index
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
        {
			ViewBag.RecentQuestions = Current.DB.QueryMultiple("SELECT TOP 5 ID, Title, ShortName FROM Questions ORDER BY Time DESC").Read<Question>();

            return View();
		}

		#endregion
	}
}