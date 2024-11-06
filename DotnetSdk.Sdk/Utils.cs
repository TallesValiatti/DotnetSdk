namespace DotnetSdk.Sdk;

public static class Utils
{
    public static void AssertNotNullOrWhiteSpace(string value)
    {
        if (value is null)
        {
            throw new ArgumentNullException();
        }
        
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be empty or contain only white-space characters");
        }
    }
}