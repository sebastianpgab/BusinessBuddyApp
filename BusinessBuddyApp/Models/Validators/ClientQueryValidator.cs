using FluentValidation;

namespace BusinessBuddyApp.Models.Validators
{
    public class ClientQueryValidator : AbstractValidator<ClientQuery>
    {
        private int[] allowedPageSizes = {5, 10, 15};
        public ClientQueryValidator()
        {
            RuleFor(p => p.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize).Custom((value, context) =>
            {
                var result = allowedPageSizes.Contains(value);
                if(!result) 
                {
                    context.AddFailure("PageSize", $"The page size must be one of the options: {string.Join(",", allowedPageSizes)}");
                }
            });
        }
    }
}
