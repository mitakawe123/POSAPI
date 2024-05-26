using POSAPI.Domain.Entities;

namespace POSAPI.Application.People.Commands.UpdatePersonCommand;
public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(v => v.FullName)
            .MaximumLength(100).WithMessage("Full name must be less than 100 characters.")
            .NotEmpty().WithMessage("Full name is required.");

        RuleFor(x => x.Addresses)
            .SetValidator(new AddressValidator())
            .NotEmpty().WithMessage("Address is required.");
    }
}
public class AddressValidator : AbstractValidator<ICollection<Address>>
{
    public AddressValidator()
    {
        RuleForEach(address => address)
            .SetValidator(new SingleAddressValidator());
    }
}

public class SingleAddressValidator : AbstractValidator<Address>
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

public class PhoneValidator : AbstractValidator<Phone>
{
    public PhoneValidator()
    {
        RuleFor(phone => phone.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{6}$").WithMessage("Phone number must be 6 digits.");
    }
}
