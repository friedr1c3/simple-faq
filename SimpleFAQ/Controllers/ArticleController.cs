using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFAQ.Controllers
{
	using Models;

	/// <summary>
	/// Article controller.
	/// </summary>
    public class ArticleController : BaseController
	{
		#region View

		/// <summary>
		/// GET: /article/{shortName}
		/// </summary>
		/// <param name="id"></param>
		/// <param name="shortName"></param>
		/// <returns></returns>
		[Route("article/{shortName}")]
		public ActionResult View(string shortName)
		{
			var question = Current.DB.Query<Question>("SELECT * FROM Questions WHERE ShortName = @ShortName", new { ShortName = shortName }).FirstOrDefault();

			// Question not found.
			if (question == null)
			{
				return NotFound();
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
		/// GET: /article/add
		/// </summary>
		/// <param name="question"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Add(Question question)
		{
			ViewBag.Edit = false;

			return View("Article");
		}

		#endregion
	}
}