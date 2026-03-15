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
            _httpClient = new HttpClient() { BaseAddress = new Uri($"{_apiUrl}") };
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> PromptModelAsync(string prompt)
        {
            OpenRouterMessageObject openRouterMessageBody = new OpenRouterMessageObject(_role, prompt);
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    model = _model,
                    messages = new List<OpenRouterMessageObject> { openRouterMessageBody }
                }),
                Encoding.UTF8,
                "application/json"
            );

            using HttpResponseMessage response = await _httpClient.PostAsync("/chat/completions",jsonContent);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse;
        }
    }

    public class OpenRouterMessageObject(string role, string content)
    {
        [JsonPropertyName("role")]
        public string _role = role;

        [JsonPropertyName("content")]
        public string _content = content;
    }
}