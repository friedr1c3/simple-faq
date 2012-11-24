using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFAQ.Controllers
{
	/// <summary>
	/// Home controller.
	/// </summary>
    public class HomeController : Controller
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
            return View();
		}

		#endregion
	}
}