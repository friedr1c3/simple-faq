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
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="identity"></param>
		/// <returns></returns>
		public static User GetCurrentUser(HttpRequest request, string identity)
		{
			return GetCurrentUser(request.IsAuthenticated, identity);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="identity"></param>
		/// <returns></returns>
		public static User GetCurrentUser(HttpRequestBase request, string identity)
		{
			return GetCurrentUser(request.IsAuthenticated, identity);
		}

		#endregion

		#region Private Methods

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
		private static User GetCurrentUser(bool isAuthenticated, string identity)
		{
			var user = new User();

			if (isAuthenticated)
			{
				int id;

				if (Int32.TryParse(identity, out id))
				{
					User lookup = Current.DB.Users.Get(id);

					if (lookup != null)
					{
						user = lookup;
					}
				}

				else
				{
					FormsAuthentication.SignOut();
				}
			}

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