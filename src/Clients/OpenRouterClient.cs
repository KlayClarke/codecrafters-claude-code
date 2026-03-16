using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;

namespace OpenRouterClient
{
    public class OpenRouterClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly string _model;
        private readonly string _role = "user";

        public OpenRouterClient(string apiKey, string apiUrl, string model)
        {
            _apiKey = apiKey;
            _apiUrl = apiUrl;
            _model = model;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> PromptModelAsync(string prompt)
        {
            OpenRouterMessageObject openRouterMessageBody = new OpenRouterMessageObject { Role = _role, Content = prompt};
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    model = _model,
                    messages = new List<OpenRouterMessageObject> { openRouterMessageBody }
                }),
                Encoding.UTF8,
                "application/json"
            );

            using HttpResponseMessage response = await _httpClient.PostAsync($"{_apiUrl}" + "/chat/completions", jsonContent);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse;
        }
    }

    class OpenRouterMessageObject
    {
        [JsonPropertyName("role")]
        public required string Role { get; set; }

        [JsonPropertyName("content")]
        public required string Content { get; set; }
    }

    class OpenRouterResponseObject
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }
        
        [JsonPropertyName("choices")]
        public required string Choices { get; set; }

        [JsonPropertyName("message")]
        public required string Message { get; set; }

        [JsonPropertyName("usage")]
        public required string Usage { get; set; }
    }
}