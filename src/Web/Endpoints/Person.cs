using POSAPI.Application.Person.Commands.CreatePersonCommand;
using POSAPI.Application.Person.Queries;

namespace POSAPI.Web.Endpoints;

public class Person : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetPeople)
            .MapPost(CreatePerson);
    }

    public Task<IEnumerable<Domain.Entities.Person>> GetPeople(ISender sender)
    {
        return sender.Send(new GetPeopleQuery());
    }

    public Task<Guid> CreatePerson(ISender sender, CreatePersonCommand command)
    {
        return sender.Send(command);
    }
}
