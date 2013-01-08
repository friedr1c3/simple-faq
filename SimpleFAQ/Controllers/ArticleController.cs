using MarkdownSharp;
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
	using ViewModels;

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
			if (question.Password.HasValue())
			{
				if (Session[passwordKey] == null)
				{
					return RedirectToAction("password", new { id = id, shortName = shortName });
				}
			}

			// Increment view count for question.
			question.Views = question.Views + 1;

			// Update question.
			Current.DB.Questions.Update(id, new { question.Views });

			var md = new Markdown();

			question.Answer = md.Transform(question.Answer);

			var vq = new ViewQuestion { Question = question, User = Current.DB.Users.Get(question.OwnerUserID) };

			return View(vq);
		}

		#endregion

		#region Password

		/// <summary>
		/// GET: /article/{id}/{shortName}/password
		/// </summary>
		/// <param name="id"></param>
		/// <param name="shortName"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("article/{id:INT}/{shortName}/password")]
		public ActionResult Password(int id, string shortName)
		{
			// Get question from database.
			var question = Current.DB.Questions.Get(id);

			if (question == null)
			{
				return NotFound();
			}

			ViewBag.ID = id;
			ViewBag.ShortName = shortName;

			return View();
		}

		/// <summary>
		/// POST: /article/{id}/{shortName}/password
		/// </summary>
		/// <param name="id"></param>
		/// <param name="shortName"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("article/{id:INT}/{shortName}/password")]
		public ActionResult Password(FormCollection formCollection, int id, string shortName)
		{
			// Get question from database.
			var question = Current.DB.Questions.Get(id);

			if (question == null)
			{
				return NotFound();
			}

			ViewBag.ID = id;
			ViewBag.ShortName = shortName;

			var password = formCollection["Password"];

			if (!password.HasValue())
			{
				this.ModelState.AddModelError("Password", "Incorrect password. Try again.");

				return View();
			}

			password = Encryption.ComputerHash(password, new SHA256CryptoServiceProvider(), Encoding.UTF8.GetBytes(CANNED_SALT));

			if (password != question.Password)
			{
				this.ModelState.AddModelError("Password", "Incorrect password. Try again.");

				return View();
			}

			string passwordKey = "Question:" + id;

			// Add password to session.
			Session.Add(passwordKey, password);

			return RedirectToAction("view", new { id = id, shortName = shortName });
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
		[ValidateInput(false)]
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

				// Increment question count for the current user.
				this.CurrentUser.Questions = this.CurrentUser.Questions + 1;

				Current.DB.Users.Update(this.CurrentUser.ID, new { this.CurrentUser.Questions });

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
		/// GET: /article/{id}/{shortName}/edit
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("article/{id:INT}/{shortName}/edit")]
		public ActionResult Edit(int id, string shortName)
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
		/// POST: /article/{id}/{shortName}/edit
		/// </summary>
		/// <param name="question"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateInput(false)]
		[Route("article/{id:INT}/{shortName}/edit")]
		public ActionResult Edit(int id, string shortName, Question question)
		{
			ViewBag.Edit = true;

			// Get original question in former state from the database.
			var dbQuestion = Current.DB.Questions.Get(id);

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

				else
				{
					question.Password = dbQuestion.Password;
				}

				// Remove password if the checkbox has been ticked.
				if (question.RemovePassword)
				{
					question.Password = null;
				}

				question.ShortName = URLHelpers.ToURLFragment(question.Title, 100);

				// Update
				Current.DB.Questions.Update(id, new { question.Title, question.Answer, question.ShortName, question.Password });

				return RedirectToAction("view", new { id = id, shortName = question.ShortName });
			}

			else
			{
				return View("Article");
			}
		}

		#endregion

		#region Delete

		/// <summary>
		/// GET: /article/{id}/{shortName}/delete
		/// </summary>
		/// <param name="id"></param>
		/// <param name="shortName"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("article/{id:INT}/{shortName}/delete")]
		public ActionResult Delete(int id, string shortName)
		{
			return View();
		}

		/// <summary>
		/// POST: article/{id}/{shortName}/delete
		/// </summary>
		/// <param name="formCollection"></param>
		/// <param name="id"></param>
		/// <param name="shortName"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("article/{id:INT}/{shortName}/delete")]
		public ActionResult Delete(FormCollection formCollection, int id, string shortName)
		{
			return GenericMessage("Article has been successfully deleted.", "/");
		}

		#endregion
	}
}