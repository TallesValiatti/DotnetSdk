using System.Net.Http.Json;
using DotnetSdk.Common.Models;

namespace DotnetSdk.Sdk.Client;

public partial class AssessReviewClient
{
    public async Task<ReviewResult> CreateReviewAsync(ReviewRequest request, CancellationToken cancellationToken = default)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/assess-reviews")
        {
            Content = JsonContent.Create(request)
        };

        using HttpResponseMessage response = await _client.SendAsync(httpRequest, cancellationToken);
        
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ReviewResult>(cancellationToken: cancellationToken) ?? throw new InvalidOperationException("Failed to deserialize response");
    }
}