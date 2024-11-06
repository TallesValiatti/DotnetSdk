using DotnetSdk.Common;
using DotnetSdk.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSdk.Api;

public static class Endpoints
{
    private const int PageSize = 5;
    
    public static WebApplication UseAppEndpoints(this WebApplication app)
    {
        app.MapPost("/assess-reviews",
            async ([FromServices] AiService.AiService service, [FromServices] Repository repository, [FromBody] ReviewRequest request) =>
            {
                var result = await service.AssessReviewAsync(request);
                repository.Add(result);
                
                return Results.Ok(result);
            })
            .WithName("AssessReview")
            .WithOpenApi()
            .AddEndpointFilter<ApiKeyEndpointFilter>();
        
        app.MapGet("/assess-reviews", ([FromServices] Repository repository, int? page) =>
                {
                    page ??= 1;
                    
                    var items = repository.List();
                    
                    var count = items.Count;

                    var data = items
                        .Skip((page.Value - 1) * PageSize)
                        .Take(page.Value)
                        .ToList();
                    
                    var paginatedItems = PaginatedListUtils<ReviewResult>.Create(data, count, page.Value, PageSize);
                    
                    return Results.Ok(new
                    {
                        paginatedItems.PageIndex,
                        paginatedItems.TotalPages,
                        paginatedItems.HasNextPage,
                        paginatedItems.HasPreviousPage,
                        data = paginatedItems,
                    });
                })
            .WithName("ListAssessReview")
            .WithOpenApi()
            .AddEndpointFilter<ApiKeyEndpointFilter>();

        return app;
    }
}