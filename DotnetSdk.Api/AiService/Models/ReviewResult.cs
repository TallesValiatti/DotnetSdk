namespace DotnetSdk.Api.AiService.Models;

public record ReviewResult(ReviewResultType Type, decimal TokenUsageCost)
{
    public string Description => Type.ToString();
};

public enum ReviewResultType
{
    VeryBad = 1,
    Bad,
    Medium,
    Good,
    VeryGood,
    Unknown
}