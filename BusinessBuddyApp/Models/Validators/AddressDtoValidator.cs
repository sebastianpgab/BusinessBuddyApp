using FluentValidation;

namespace BusinessBuddyApp.Models.Validators
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(p => p.City).NotEmpty();
            RuleFor(p => p.Street).NotEmpty();
            RuleFor(p => p.PostalCode).NotEmpty();
            RuleFor(p => p).Custom((address, context) =>
            {
                bool isApartmentNumberFilled = !string.IsNullOrEmpty(address.ApartmentNumber) ;
                bool isBuildingNumberFilled = !string.IsNullOrEmpty(address.BuildingNumber);

                if(!(isApartmentNumberFilled ^ isBuildingNumberFilled))
                {
                    context.AddFailure("Address", "You must fill in either the building number or the apartment number, but not both");
                }
            });
        }
    }
}
