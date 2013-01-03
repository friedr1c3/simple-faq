using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FluentValidation.Mvc;

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
		public ActionResult Login([CustomizeValidator(RuleSet = "Login")]User user)
		{
			if (this.ModelState.IsValid)
			{
				// Get information for the user by the specified username.
				var dbUser = Current.DB.Query<User>("SELECT * FROM Users WHERE Username = @Username", new { user.Username }).FirstOrDefault();

				if (dbUser != null)
				{
					// Encrypt password.
					user.Password = Encryption.ComputerHash(user.Password, new SHA256CryptoServiceProvider(), Encoding.UTF8.GetBytes(dbUser.Salt));

					// Compare passwords
					if (user.Password != dbUser.Password)
					{
						this.ModelState.AddModelError("Username", "Incorrect username or password. Try again.");

						return View(user);
					}

					//var ticket = new FormsAuthenticationTicket(1, dbUser.ID.ToString(), DateTime.Now, DateTime.Now.AddYears(1), true, null);

					return GenericMessage("You have successfully logged in!", "/");
				}

				else
				{
					return View();
				}
			}

			else
			{
				return View(user);
			}
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
		public ActionResult Register([CustomizeValidator(RuleSet = "Registration")]User user)
		{
			if (this.ModelState.IsValid)
			{
				user.Salt = Utilities.RandomString(10);
				user.Password = Encryption.ComputerHash(user.Password, new SHA256CryptoServiceProvider(), Encoding.UTF8.GetBytes(user.Salt));
				user.IPAddress = this.HttpContext.Request.UserHostAddress;

				// Insert user into database.
				Current.DB.Users.Insert(new { user.Username, user.Email, user.Password, user.Salt, user.IPAddress });

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