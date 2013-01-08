using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFAQ.Controllers
{
	using Core.Helpers;
	using Models;

	/// <summary>
	/// Search controller.
	/// </summary>
    public class SearchController : BaseController
	{
		#region Index

		/// <summary>
		/// GET: /search
		/// GET: /search/index
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(string input, int? page, int? pageSize)
        {
			ViewBag.Input = input;

			page = Math.Max(page ?? 1, 1); // Adjust.
			pageSize = Math.Max(Math.Min(pageSize ?? 10, 100), 10); // Adjust.

			int start = ((page.Value - 1) * pageSize.Value) + 1;
			int finish = page.Value * pageSize.Value;
			var builder = new SqlBuilder();
			SqlBuilder.Template pager = null, counter = null;

			pager = builder.AddTemplate("SELECT * FROM (SELECT /**select**/, ROW_NUMBER() OVER (/**orderby**/) AS RowNumber FROM Questions Q /**where**/) AS Results WHERE RowNumber BETWEEN @start AND @finish ORDER BY RowNumber", 
				new { start = start, finish = finish });

			counter = builder.AddTemplate("SELECT COUNT(*) FROM Questions Q /**where**/");

			builder.Select("Q.*");
			builder.OrderBy("Q.Time DESC");
			builder.Where("Q.Title LIKE @searchInput OR Q.Answer LIKE @searchInput", new { searchInput = "%" + input + "%" });

			var results = Current.DB.QueryMultiple(pager.RawSql, pager.Parameters).Read<Question>();
			var totals = Current.DB.Query<int>(counter.RawSql, counter.Parameters).First();

			ViewData["Href"] = "/search?";

			return View(new PagedList<Question>(results, page.Value, pageSize.Value, false, totals));
		}

		#endregion
	}
}