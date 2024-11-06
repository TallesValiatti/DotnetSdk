using DotnetSdk.Common;

namespace DotnetSdk.Sdk.Client;

public partial class AssessReviewClient : IAssessReviewClient
{
    private readonly HttpClient _client;
    
    private AssessReviewClient(string endpoint, string apikey)
    {
        Utils.AssertNotNullOrWhiteSpace(endpoint);
        Utils.AssertNotNullOrWhiteSpace(apikey);
        
        _client = new(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromSeconds(10)
        })
        {
            BaseAddress = new Uri(endpoint.Trim()),
            DefaultRequestHeaders =
            {
                { "accept", "application/json" },
                { Constants.ApiKeyHeader, apikey.Trim() }
            },
            Timeout = TimeSpan.FromSeconds(15),
        };
    }

    public static AssessReviewClient Create(string endpoint, string apikey)
    {
        return new AssessReviewClient(endpoint, apikey);
    }
}