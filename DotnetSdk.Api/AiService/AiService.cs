using Azure;
using Azure.AI.OpenAI;
using DotnetSdk.Common.Models;
using OpenAI.Chat;

namespace DotnetSdk.Api.AiService;

public class AiService(IConfiguration configuration)
{
    private const string SystemPrompt = 
        """
        You are an expert in evaluating customer feedback on restaurant dishes. Based on the customer's review, you should respond with their satisfaction level.
        
        Options for your response:
        
        VeryBad = 1
        Bad = 2
        Medium = 3
        Good = 4
        VeryGood = 5
        Unknown = 6
        
        If you cannot assess, reply 'Unknown' (6)
        Reply with only the corresponding number.
        
        Example 1
        Message: This is the best food in the world!
        Result: 5
        
        Example 2
        Message: This food is not bad, but it’s nothing spectacular
        Result: 3
        
        Example 3
        Message: What is my name?
        Result: 6
        """;
    
    public async Task<ReviewResult> AssessReviewAsync(ReviewRequest reviewRequest)
    {
        var client = GetClient();
        
        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage(SystemPrompt),
            new UserChatMessage(reviewRequest.Message)
        ]);

        var result = completion.Content[0].Text;
        
        return new ReviewResult(
            reviewRequest.Message,
            (ReviewResultType)Convert.ToInt16(result),
            CalculateTokenUsageCost(completion));
    }

    private decimal CalculateTokenUsageCost(ChatCompletion completion)
    {
        var inputPrice = configuration.GetValue<decimal>("AzureOpenAi:InputPrice");
        var outputPrice = configuration.GetValue<decimal>("AzureOpenAi:OutputPrice");
        
        return inputPrice * completion.Usage.InputTokens + outputPrice * completion.Usage.OutputTokens * outputPrice;
    }

    private ChatClient GetClient()
    {
        string endpoint = configuration["AzureOpenAi:Endpoint"]!;
        string key = configuration["AzureOpenAi:Key"]!;

        AzureOpenAIClient azureClient = new(new Uri(endpoint), new AzureKeyCredential(key));
        
        return azureClient.GetChatClient(configuration["AzureOpenAi:ImageModel"]!);
    }
}