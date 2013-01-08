using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SimpleFAQ.Controllers
{
	using Core.Helpers;
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

			var user = Current.DB.Users.Get(id);

			if (user == null)
			{
				return NotFound();
			}

			user.Password = String.Empty;
			user.PasswordConfirm = String.Empty;

			return View(user);
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

			if (this.ModelState.IsValid)
			{
				var dbUser = Current.DB.Users.Get(id);

				// User is changing their password.
				if (user.Password.HasValue())
				{
					// Passwords do not match.
					if (user.PasswordConfirm != user.Password)
					{
						this.ModelState.AddModelError("Password", "Passwords do not match.");

						return View(user);
					}

					// Generate new salt for this password.
					user.Salt = Utilities.RandomString();

					// Encrypt password.
					user.Password = Encryption.ComputerHash(user.Password, new SHA256CryptoServiceProvider(), Encoding.UTF8.GetBytes(user.Salt));
				}

				else
				{
					// Don't change salt if the user is not changing their password.
					user.Salt = dbUser.Salt;
				}

				// Update user information.
				Current.DB.Users.Update(id, new { user.Email, user.Password, user.Salt });

				return GenericMessage("Profile successfully updated.", "/user/" + id);
			}

			else
			{
				return View(user);
			}
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