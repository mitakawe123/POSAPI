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
            .MapGet(GetPerson,"{id}")
            .MapPost(CreatePerson)
            .MapDelete(DeletePerson, "{id}")
            .MapPatch("{id}", UpdatePerson);
    }
    
    private Task<PersonDTO> GetPerson(ISender sender, Guid id)
    {
        return sender.Send(new GetPeopleQuery(id));
    }

    private Task<Guid> CreatePerson(ISender sender, CreatePersonCommand command)
    {
        return sender.Send(command);
    }

    private async Task<IResult> UpdatePerson(ISender sender, Guid id, UpdatePersonCommand command)
    {
        if (id != command.Id) 
            return Results.BadRequest();
        
        await sender.Send(command);
        return Results.NoContent();
    }

    private async Task<IResult> DeletePerson(ISender sender, Guid id)
    {
        await sender.Send(new DeletePersonCommand(id));
        return Results.NoContent();
    }
}
