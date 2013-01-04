using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFAQ.Controllers
{
	using Models;

	/// <summary>
	/// User controller.
	/// </summary>
    public class UserController : BaseController
	{
		#region Profile

		/// <summary>
		/// GET: /user/{id}
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Route("user/{id:INT}")]
		public ActionResult Profile(int id)
		{
			// Get user.
			var user = Current.DB.Users.Get(id);

			// User cannot be found.
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		#endregion
	}
}