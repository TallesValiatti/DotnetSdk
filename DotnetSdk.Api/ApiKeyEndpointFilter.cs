namespace DotnetSdk.Api;

public class ApiKeyEndpointFilter(IConfiguration configuration) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var authKey = configuration["AuthKey"];
        
        // Key not provided
        if (!context.HttpContext.Request.Headers.TryGetValue(Constants.ApiKeyHeader, out var extractedAuthKey))
        {
            return Results.Unauthorized();
        }

        // Invalid key
        if (!authKey!.Equals(extractedAuthKey))
        {
            return Results.Unauthorized();
        }
        
        return await next(context);
    }
}