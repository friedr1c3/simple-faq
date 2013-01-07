using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFAQ.Controllers
{
	/// <summary>
	/// Search controller.
	/// </summary>
    public class SearchController : BaseController
	{
		#region Index

		/// <summary>
		/// GET: /search
		/// GET: /search/index
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(string input)
        {
			ViewBag.Input = input;

            return View();
		}

		#endregion
	}
}