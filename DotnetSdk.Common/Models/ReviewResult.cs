using System.Text.Json.Serialization;

namespace DotnetSdk.Common.Models;

public record ReviewResult(string Message ,ReviewResultType Type, decimal TokenUsageCost)
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