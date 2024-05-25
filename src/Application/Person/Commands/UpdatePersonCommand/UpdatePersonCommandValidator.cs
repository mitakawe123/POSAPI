namespace POSAPI.Application.Person.Commands.UpdatePersonCommand;
public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(v => v.FullName)
            .MaximumLength(100)
            .NotEmpty();
    }
}
