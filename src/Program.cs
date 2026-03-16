using ArgumentParser;
using OpenRouterClient;
using DotNetEnv;
using System.Threading.Tasks;

namespace ClaudeCodeCrafters;

class Program
{
    static async Task Main(string[] args)
    {
        // Parse arguments to get prompt
        string prompt = ArgumentParser.ArgumentParser.ParseArgs(args);

        // Get env variables
        Env.Load();
        string apiKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY")
            ?? throw new Exception("Missing OPENROUTER_API_KEY");
        string apiUrl = Environment.GetEnvironmentVariable("OPENROUTER_API_BASE_URL")
            ?? throw new Exception("Missing OPENROUTER_API_BASE_URL");
        string model = Environment.GetEnvironmentVariable("OPENROUTER_API_MODEL")
            ?? throw new Exception("Missing OPENROUTER_API_MODEL");

        // Create connector that'll help with open router api calls
        OpenRouterClient.OpenRouterClient openRouterClient = new OpenRouterClient.OpenRouterClient(
            apiKey,
            apiUrl,
            model
        ); 

        // Prompt the model
        string modelResponse = await openRouterClient.PromptModelAsync(prompt);
        Console.WriteLine($"modelResponse: {modelResponse}");
    }
}
