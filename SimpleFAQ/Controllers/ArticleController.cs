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
	/// Article controller.
	/// </summary>
    public class ArticleController : BaseController
	{
		#region Constants

		/// <summary>
		/// The CANNED_SALT.
		/// </summary>
		private const string CANNED_SALT = "&@JhNku44&FH^1J0jrL_";

		#endregion

		#region View

		/// <summary>
		/// GET: /article/{shortName}
		/// </summary>
		/// <param name="id"></param>
		/// <param name="shortName"></param>
		/// <returns></returns>
		[Route("article/{id:INT}/{shortName}")]
		public ActionResult View(int id, string shortName)
		{
			// Get question from database.
			var question = Current.DB.Questions.Get(id);

			// Question not found.
			if (question == null)
			{
				return NotFound();
			}

			// Session key.
			string passwordKey = "Question:" + id;

			// Question has a password and the session doesn't exist.
			if (question.Password.HasValue() && Session[passwordKey] == null)
			{

			}

			return View(question);
		}

		#endregion

		#region Add

		/// <summary>
		/// GET: /article/add
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Add()
		{
			ViewBag.Edit = false;

			return View("Article");
		}

		/// <summary>
		/// POST: /article/add
		/// </summary>
		/// <param name="question"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Add(Question question)
		{
			ViewBag.Edit = false;

			if (this.ModelState.IsValid)
			{
				// A password is being assigned.
				if (question.Password.HasValue())
				{
					// If passwords don't match.
					if (question.PasswordConfirm != question.Password)
					{
						this.ModelState.AddModelError("Password", "Passwords do not match.");

						return View("Article", question);
					}

					else
					{
						question.Password = Encryption.ComputerHash(question.Password, new SHA256CryptoServiceProvider(), Encoding.UTF8.GetBytes(CANNED_SALT));
					}
				}

				question.ShortName = URLHelpers.ToURLFragment(question.Title, 100);
				question.OwnerUserID = this.CurrentUser.ID;

				// Insert question into database.
				var id = Current.DB.Questions.Insert(new { question.Title, question.Answer, question.OwnerUserID, question.ShortName, question.Password });

				return RedirectToAction("view", new { id = id, shortName = question.ShortName });
			}

			else
			{
				return View("Article", question);
			}
		}

		#endregion

		#region Edit

		/// <summary>
		/// GET: /article/edit/{id}
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Edit(int id)
		{
			ViewBag.Edit = true;

			// Get question from database.
			var question = Current.DB.Questions.Get(id);

			// Not found.
			if (question == null)
			{
				return NotFound();
			}

			// Remove password input.
			question.Password = String.Empty;
			question.PasswordConfirm = String.Empty;

			return View("Article", question);
		}

		/// <summary>
		/// POST: /article/edit
		/// </summary>
		/// <param name="question"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Edit(Question question)
		{
			ViewBag.Edit = true;

			// Password input null = do not update password.
			// Password input string = update password.
			// Remove password ticked = remove password (set to NULL).

			return View("Article");
		}

		#endregion
	}
}