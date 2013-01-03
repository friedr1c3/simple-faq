using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleFAQ.Controllers
{
	using Core.Helpers;
	using Models;

	/// <summary>
	/// Auth controller.
	/// </summary>
    public class AuthController : BaseController
	{
		#region Login

		/// <summary>
		/// GET: /auth/login
		/// </summary>
		/// <returns></returns>
		public ActionResult Login()
		{
			return View();
		}

		/// <summary>
		/// POST: /auth/login
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Login(FormCollection formCollection)
		{
			return View();
		}

		#endregion

		#region Logout

		/// <summary>
		/// GET: /auth/logout
		/// </summary>
		/// <returns></returns>
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("index", "home");
		}

		#endregion

		#region Register

		/// <summary>
		/// GET: /auth/register
		/// </summary>
		/// <returns></returns>
		public ActionResult Register()
		{
			return View();
		}

		/// <summary>
		/// POST: /auth/register
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Register(User user)
		{
			if (this.ModelState.IsValid)
			{
				user.Salt = Utilities.RandomString(10);
				user.Password = Encryption.ComputerHash(user.Password, new SHA256CryptoServiceProvider(), Encoding.UTF8.GetBytes(user.Salt));

				return GenericMessage("You have successfully registered!", "/");
			}

			else
			{
				// Passwords do not match.
				if (user.PasswordConfirm != user.Password)
				{
					ModelState.AddModelError("PasswordConfirm", "Confirmation password does not match original password.");
				}

				return View(user);
			}
		}

		#endregion
	}
}