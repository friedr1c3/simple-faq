using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleFAQ.Validators
{
	using Models;

	/// <summary>
	/// Question validator.
	/// </summary>
	public class QuestionValidator : AbstractValidator<Question>
	{
		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public QuestionValidator()
		{
			RuleFor(x => x.Title)
				.NotNull()
				.WithMessage("Enter a question title.")
				.Length(2, 250)
				.WithMessage("Title must be between 2 and 250 characters.");

			RuleFor(x => x.Answer)
				.NotNull()
				.WithMessage("Enter an answer.")
				.Length(2, 50000)
				.WithMessage("Answer body must be between 2 and 50,000 characters.");

			// Password is optional.
			RuleFor(x => x.Password)
				.Length(0, 16)
				.WithMessage("Password must not exceed 16 characters.");
		}

		#endregion
	}
}