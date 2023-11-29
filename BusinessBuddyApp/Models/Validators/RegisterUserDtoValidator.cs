using BusinessBuddyApp.Entities;
using FluentValidation;

namespace BusinessBuddyApp.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(BusinessBudyDbContext dbContext)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.PasswordConfirmed).Equal(e => e.Password);
            RuleFor(p => p.Email).Custom((value, context) =>
            {
                var isTaken = dbContext.Users.Any(p => p.Email == value);
                if(isTaken)
                {
                    context.AddFailure("Email", "This email address is already in use");
                }
            });
        }
    }
}
