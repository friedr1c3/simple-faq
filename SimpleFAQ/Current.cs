using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Caching;

namespace SimpleFAQ
{
	using Controllers;
	using Core.Database;
	using Models;

	/// <summary>
	/// 
	/// </summary>
	public static class Current
	{
		#region Fields & Constants

		/// <summary>
		/// 
		/// </summary>
		const string DISPOSE_CONNECTION_KEY = "dispose_connections";

		#endregion

		#region Public Methods

		public static void RegisterConnectionForDisposal(SqlConnection connection)
		{
			List<SqlConnection> connections = Context.Items[DISPOSE_CONNECTION_KEY] as List<SqlConnection>;
			if (connections == null)
			{
				Context.Items[DISPOSE_CONNECTION_KEY] = connections = new List<SqlConnection>();
			}

			connections.Add(connection);
		}

		public static void DisposeRegisteredConnections()
		{
			List<SqlConnection> connections = Context.Items[DISPOSE_CONNECTION_KEY] as List<SqlConnection>;

			if (connections != null)
			{
				Context.Items[DISPOSE_CONNECTION_KEY] = null;

				foreach (var connection in connections)
				{
					try
					{
						if (connection.State != ConnectionState.Closed)
						{
						}

						connection.Dispose();
					}

					catch
					{
						/* don't care, nothing we can do */
					}
				}
			}
		}

		/// <summary>
		/// Allows end of reqeust code to clean up this request's DB.
		/// </summary>
		public static void DisposeDB()
		{
			SimpleFAQDatabase db = null;

			if (Context != null)
			{
				db = Context.Items["DB"] as SimpleFAQDatabase;
			}

			else
			{
				db = CallContext.GetData("DB") as SimpleFAQDatabase;
			}

			if (db != null)
			{
				db.Dispose();

				if (Context != null)
				{
					Context.Items["DB"] = null;
				}

				else
				{
					CallContext.SetData("DB", null);
				}
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Shortcut to HttpContext.Current.
		/// </summary>
		public static HttpContext Context
		{
			get { return HttpContext.Current; }
		}

		/// <summary>
		/// Shortcut to HttpContext.Current.Request.
		/// </summary>
		public static HttpRequest Request
		{
			get { return Context.Request; }
		}

		/// <summary>
		/// Gets the controller for the current request; should be set during init of current request's controller.
		/// </summary>
		public static BaseController Controller
		{
			get { return Context.Items["Controller"] as BaseController; }
			set { Context.Items["Controller"] = value; }
		}

		/// <summary>
		/// Gets the current "authenticated" user from this request's controller.
		/// </summary>
		public static User User
		{
			get
			{
				if (Controller == null)
				{
					return BaseController.GetCurrentUser(HttpContext.Current.Request, HttpContext.Current.User.Identity.Name);
				}

				return Controller.CurrentUser;
			}
		}

		/// <summary>
		/// Gets the single data context for this current request.
		/// </summary>
		public static SimpleFAQDatabase DB
		{
			get
			{
				SimpleFAQDatabase result = null;
				
				if (Context != null)
				{
					result = Context.Items["DB"] as SimpleFAQDatabase;
				}

				else
				{
					// unit tests
					result = CallContext.GetData("DB") as SimpleFAQDatabase;
				}

				if (result == null)
				{
					DbConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

					cnn.Open();
					result = SimpleFAQDatabase.Create(cnn, 30);

					if (Context != null)
					{
						Context.Items["DB"] = result;
					}

					else
					{
						CallContext.SetData("DB", result);
					}
				}

				return result;
			}
		}

		#endregion
	}
}