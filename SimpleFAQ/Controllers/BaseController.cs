using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleFAQ.Controllers
{
	using Models;

	/// <summary>
	/// Base controller.
	/// </summary>
    public class BaseController : Controller
	{
		#region Fields

		/// <summary>
		/// The _currentUser.
		/// </summary>
		private User _currentUser;

		#endregion

		#region Public Methods

		/// <summary>
		/// Returns a generic message view.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		public ActionResult GenericMessage(string message, string url)
		{
			var gm = new GenericMessage()
			{
				Message = message,
				URL = url
			};

			return View("GenericMessage", gm);
		}

		/// <summary>
		/// Not found (404) content result.
		/// </summary>
		/// <returns></returns>
		public ContentResult NotFound()
		{
			return new ContentResult { Content = "404", ContentType = "text/plain" };
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="identity"></param>
		/// <returns></returns>
		public static User GetCurrentUser(HttpRequest request, string identity)
		{
			return GetCurrentUser(request.IsAuthenticated, request.UserHostAddress, identity);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="identity"></param>
		/// <returns></returns>
		public static User GetCurrentUser(HttpRequestBase request, string identity)
		{
			return GetCurrentUser(request.IsAuthenticated, request.UserHostAddress, identity);
		}

		#endregion

		#region Private Methods

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			Current.Controller = this;

			base.Initialize(requestContext);
		}

		/// <summary>
		/// Initializes the current user.
		/// </summary>
		protected void InitCurrentUser()
		{
			this._currentUser = GetCurrentUser(Request, User.Identity.Name);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="isAuthenticated"></param>
		/// <param name="identity"></param>
		/// <returns></returns>
		private static User GetCurrentUser(bool isAuthenticated, string userIP, string identity)
		{
			var user = new User();
			user.IsAnonymous = true;

			if (isAuthenticated)
			{
				int id;

				if (Int32.TryParse(identity, out id))
				{
					User lookup = Current.DB.Users.Get(id);

					if (lookup != null)
					{
						user = lookup;
						user.IsAnonymous = false;
					}
				}

				else
				{
					FormsAuthentication.SignOut();
				}
			}

			user.IPAddress = userIP;

			return user;
		}


		#endregion

		#region Properties

		/// <summary>
		/// Gets the current user.
		/// </summary>
		public User CurrentUser
		{
			get
			{
				if (_currentUser == null)
				{
					InitCurrentUser();
				}

				return _currentUser;
			}
		}

		#endregion
	}
}