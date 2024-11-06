using DotnetSdk.Common;
using DotnetSdk.Common.Models;

namespace DotnetSdk.Sdk.Client;

public interface IAssessReviewClient
{
    Task<ReviewResult> CreateReviewAsync(ReviewRequest request, CancellationToken cancellationToken = default);
    Task<PaginatedListUtils<ReviewResult>> ListAssessReviewsAsync(int page = 1, CancellationToken cancellationToken = default);
}