using POSAPI.Application.Common.Interfaces;

namespace POSAPI.Application.Person.Commands.CreatePersonCommand;

public record CreatePersonCommand(string FullName) : IRequest<int>;

public class CreatePersonCommandHandler(IApplicationDbContext context) :
    IRequestHandler<CreatePersonCommand, int>
{
    public Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        return new Task<int>(() => 1);
    }
}
