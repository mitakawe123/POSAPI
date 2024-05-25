using POSAPI.Application.Person.Commands.CreatePersonCommand;
using POSAPI.Application.Person.Commands.DeletePersonCommand;
using POSAPI.Application.Person.Commands.UpdatePersonCommand;
using POSAPI.Application.Person.Queries;

namespace POSAPI.Web.Endpoints;

public class Person : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetPeople)
            .MapPost(CreatePerson)
            .MapDelete(DeletePerson, "{id}")
            .MapPatch("{id}", UpdatePerson);
    }

    public Task<IEnumerable<Domain.Entities.Person>> GetPeople(ISender sender)
    {
        return sender.Send(new GetPeopleQuery());
    }

    public Task<Guid> CreatePerson(ISender sender, CreatePersonCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdatePerson(ISender sender, Guid id, UpdatePersonCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeletePerson(ISender sender, Guid id)
    {
        await sender.Send(new DeletePersonCommand(id));
        return Results.NoContent();
    }
}
