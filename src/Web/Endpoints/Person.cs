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
    
    private Task<string> GetPerson(ISender sender, Guid id)
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

/*{
"fullName": " Burns, David Goss",
"addresses": [
{
    "street": ": 114 Hill St.",
    "city": "Anytown",
    "state": "CA",
    "zipCode": "12345",
    "country": "USA",
    "type": 1,
    "phones": [
    {
        "phoneNumber": "230235"
    },
    {
        "phoneNumber": "109736"
    }
    ]
},
{
"street": "110 Runnymede St.",
"city": "Othertown",
"state": "NY",
"zipCode": "67890",
"country": "USA",
"type": 0
},
{
    "street": "20 Lester St.",
    "city": "Othertown",
    "state": "NY",
    "zipCode": "67890",
    "country": "USA",
    "type": 0,
    "phones": [
    {
        "phoneNumber": "232278"
    }
    ]
}
]
}*/

