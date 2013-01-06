using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFAQ.Controllers
{
	/// <summary>
	/// Admin controller.
	/// </summary>
    public class AdminController : BaseController
	{
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{	
			if (this.CurrentUser.IsAnonymous || !this.CurrentUser.IsAdmin)
			{
				filterContext.Result = NotFound();
			}

			base.OnActionExecuting(filterContext);
		}

		#region Index

		/// <summary>
		/// GET: /admin
		/// GET: /admin/index
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return View();
		}

		#endregion

		#region Settings

		/// <summary>
		/// GET: /admin/settings
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("admin/settings")]
		public ActionResult Settings()
		{
			return View();
		}

		#endregion

		#region Refresh Settings

		/// <summary>
		/// GET: /admin/settings/refresh
		/// </summary>
		/// <returns></returns>
		[Route("admin/settings/refresh")]
		public ActionResult Refresh_Settings()
		{
			AppSettings.Refresh();

			return GenericMessage("Application settings have been refreshed.", "/admin");
		}

		#endregion
	}
}