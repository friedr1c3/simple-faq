using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Mvc;

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

		#region Edit

		/// <summary>
		/// GET: /user/{id}/edit
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("user/{id:INT}/edit")]
		public ActionResult Edit(int id)
		{
			// Anonymous, not an admin or not the current user.
			if (this.CurrentUser.IsAnonymous || !this.CurrentUser.IsAdmin || (this.CurrentUser.ID != id))
			{
				return RedirectToAction("profile", new { id = id });
			}

			return View();
		}

		/// <summary>
		/// POST: /user/{id}/edit
		/// </summary>
		/// <param name="user"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("user/{id:INT}/edit")]
		public ActionResult Edit([CustomizeValidator(RuleSet = "Edit")]User user, int id)
		{
			// Anonymous, not an admin or not the current user.
			if (this.CurrentUser.IsAnonymous || !this.CurrentUser.IsAdmin || (this.CurrentUser.ID != id))
			{
				return RedirectToAction("profile", new { id = id });
			}

			return GenericMessage("Profile successfully updated.", "/user/" + id);
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

			// Anonymous, not an admin or not the current user.
			if (this.CurrentUser.IsAnonymous || !this.CurrentUser.IsAdmin || (this.CurrentUser.ID != id))
			{
				return RedirectToAction("profile", new { id = id });
			}

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
			// Anonymous, not an admin or not the current user.
			if (this.CurrentUser.IsAnonymous || !this.CurrentUser.IsAdmin || (this.CurrentUser.ID != id))
			{
				return RedirectToAction("profile", new { id = id });
			}

			// Logout current user if they're deleting their own account.
			if (id == this.CurrentUser.ID)
			{
				// Logout
				System.Web.Security.FormsAuthentication.SignOut();
			}

			// Delete user.
			Current.DB.Users.Delete(id);

			// Delete questions.
			Current.DB.Query("DELETE FROM Questions WHERE OwnerUserID = @OwnerId", new { OwnerId = id });

			return GenericMessage("User profile has successfully been deleted. This action cannot be undone.", "/");
		}

		#endregion
	}
}