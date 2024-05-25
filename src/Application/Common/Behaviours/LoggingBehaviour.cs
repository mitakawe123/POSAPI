using Serilog;

namespace POSAPI.Application.Common.Behaviours;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Log the request
        Log.Information("Handling {RequestName} with request: {@Request}", typeof(TRequest).Name, request);

        var response = await next();

        // Log the response
        Log.Information("Handled {RequestName} with response: {@Response}", typeof(TRequest).Name, response);

        return response;
    }
}
