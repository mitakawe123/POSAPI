using POSAPI.Application.Common.Interfaces;

namespace POSAPI.Web.Endpoints;

public class PhoneBook : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization();
    }
}
