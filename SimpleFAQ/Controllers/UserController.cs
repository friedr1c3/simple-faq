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

		#region Delete

		/// <summary>
		/// GET: /user/{id}/delete
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("user/{id:INT}/delete")]
		public ActionResult Delete(int id)
		{
			ViewBag.UserID = id;

			return View();
		}

		/// <summary>
		/// POST: /user/{id}/delete
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("user/{id:INT}/delete")]
		public ActionResult Delete(int id, FormCollection formCollection)
		{
			// Logout current user if they're deleting their own account.
			if (id == this.CurrentUser.ID)
			{
				// Logout
				System.Web.Security.FormsAuthentication.SignOut();
			}

			// Delete user.
			Current.DB.Users.Delete(id);

			// Delete questions.

			return GenericMessage("User profile has successfully been deleted. This action cannot be undone.", "/");
		}

		#endregion
	}
}