using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeHub.ViewModels
{
	public class RegisterViewModel: IValidatableObject
	{
		[Required(ErrorMessage = "Email required")]
		[EmailAddress(ErrorMessage = "Incorrect format")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Password required")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{10,}$",
                ErrorMessage = "Password is too easy")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password == "qwer1234")
            {
                yield return new ValidationResult("Password is too easy", new[] { "Password" });
            }
        }
    }
}

