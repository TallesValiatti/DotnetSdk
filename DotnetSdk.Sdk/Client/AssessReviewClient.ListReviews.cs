using System.Text.Json;
using DotnetSdk.Common;
using DotnetSdk.Common.Models;

namespace DotnetSdk.Sdk.Client;

public partial class AssessReviewClient
{
    public async Task<PaginatedListUtils<ReviewResult>> ListAssessReviewsAsync(int page = 1, CancellationToken cancellationToken = default)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/assess-reviews?page={page}");
        
        using HttpResponseMessage response = await _client.SendAsync(httpRequest, cancellationToken);
        response.EnsureSuccessStatusCode();
      
        string content = await response.Content.ReadAsStringAsync(cancellationToken);
        
        var paginatedResult = JsonSerializer.Deserialize<PaginatedListUtils<ReviewResult>>(
              content,
              new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
          ?? throw new InvalidOperationException("Failed to deserialize response");
            
        return paginatedResult ?? throw new InvalidOperationException("Failed to deserialize response");
    }
}