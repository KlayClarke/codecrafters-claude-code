using OpenRouterClient;
using DotNetEnv;

namespace ClaudeCodeCrafters;

class Program
{
    static void Main(string[] args)
    {
        Env.Load();
        
        string apiKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY")
            ?? throw new Exception("Missing OPENROUTER_API_KEY");
        string apiUrl = Environment.GetEnvironmentVariable("OPENROUTER_API_BASE_URL")
            ?? throw new Exception("Missing OPENROUTER_API_BASE_URL");
        string model = Environment.GetEnvironmentVariable("OPENROUTER_API_MODEL")
            ?? throw new Exception("Missing OPENROUTER_API_MODEL");

        OpenRouterClient.OpenRouterClient openRouterClient = new OpenRouterClient.OpenRouterClient(
            apiKey,
            apiUrl,
            model
        ); 
        Console.WriteLine("Hello, World!");
    }
}
