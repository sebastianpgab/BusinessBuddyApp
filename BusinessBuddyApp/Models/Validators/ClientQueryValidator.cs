using BusinessBuddyApp.Entities;
using FluentValidation;

namespace BusinessBuddyApp.Models.Validators
{
    public class ClientQueryValidator : AbstractValidator<ClientQuery>
    {
        private int[] allowedPageSizes = {5, 10, 15};
        private string[] allowedSortByColumnNames = { nameof(Client.FirstName), nameof(Client.LastName), nameof(Client.Email) };
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
            RuleFor(r => r.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value)).WithMessage($"Sort by is optional , or must be in [{string.Join(",", allowedPageSizes)}]");
        }
    }
}
