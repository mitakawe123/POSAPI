namespace POSAPI.Application.People.Commands.UpdatePersonCommand;
public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(v => v.FullName)
            .MaximumLength(100)
            .NotEmpty();
    }
}
