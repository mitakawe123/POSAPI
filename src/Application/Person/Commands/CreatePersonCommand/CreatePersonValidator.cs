namespace POSAPI.Application.Person.Commands.CreatePersonCommand;
public class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonValidator()
    {
        RuleFor(x => x.FullName)
            .MaximumLength(100).WithMessage("Full name must be less than 100 characters.")
            .NotEmpty().WithMessage("Full name is required.");

        RuleFor(x => x.Addresses)
            .SetValidator(new AddressValidator());
    }
}

public class AddressValidator : AbstractValidator<ICollection<AddressDTO>>
{
    public AddressValidator()
    {
        RuleForEach(address => address).SetValidator(new SingleAddressValidator());
    }
}

public class SingleAddressValidator : AbstractValidator<AddressDTO>
{
    public SingleAddressValidator()
    {
        RuleFor(address => address.Street)
            .NotEmpty().WithMessage("Street is required.");

        RuleFor(address => address.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(address => address.State)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(address => address.ZipCode)
            .NotEmpty().WithMessage("ZipCode is required.")
            .Matches(@"^\d{5}$").WithMessage("ZipCode must be 5 digits.");

        RuleFor(address => address.Country)
            .NotEmpty().WithMessage("Country is required.");

        RuleForEach(address => address.Phones)
            .SetValidator(new PhoneValidator());
    }
}

public class PhoneValidator : AbstractValidator<PhoneDTO>
{
    public PhoneValidator()
    {
        RuleFor(phone => phone.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits.");
    }
}
