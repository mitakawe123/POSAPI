using POSAPI.Application.Person.Queries;

namespace POSAPI.Web.Endpoints;

public class Person : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetPeople);
    }

    public Task<IEnumerable<Domain.Entities.Person>> GetPeople(ISender sender)
    {
        return sender.Send(new GetPeopleQuery());
    }
}
