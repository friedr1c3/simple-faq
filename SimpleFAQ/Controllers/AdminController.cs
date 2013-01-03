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
	}
}