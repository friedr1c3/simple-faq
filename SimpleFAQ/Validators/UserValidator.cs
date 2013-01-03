using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleFAQ.Validators
{
	using Models;

	/// <summary>
	/// User validator.
	/// </summary>
	public class UserValidator : AbstractValidator<User>
	{
		#region Constructor

		public UserValidator()
		{
			RuleSet("Registration", () =>
			{
				RuleFor(x => x.Username)
					.NotNull()
					.WithMessage("Please enter a user name.")
					.Length(2, 50)
					.WithMessage("Username must be between 2 and 50 characters.");

				RuleFor(x => x.Password)
					.NotNull()
					.WithMessage("Please enter a password.")
					.Length(4, 16)
					.WithMessage("Password must be between 4 and 16 characters.");

				RuleFor(x => x.Email)
					.NotNull()
					.WithMessage("Please enter an email address.")
					.Length(2, 100)
					.WithMessage("Email must not exceed 100 characters.")
					.EmailAddress()
					.WithMessage("Please enter a valid email address.");
			});

			RuleSet("Login", () =>
			{
				RuleFor(x => x.Username)
					.NotNull()
					.WithMessage("Please enter a user name.")
					.Length(2, 50)
					.WithMessage("Username must be between 2 and 50 characters.");

				RuleFor(x => x.Password)
					.NotNull()
					.WithMessage("Please enter a password.")
					.Length(4, 16)
					.WithMessage("Password must be between 4 and 16 characters.");	
			});
		}

		#endregion
	}
}