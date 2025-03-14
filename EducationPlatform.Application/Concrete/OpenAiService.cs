using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EducationPlatform.Application.Abstract;

namespace YourProjectNamespace.Services
{
    public class OpenAiService : IChatbotService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        // Constructor'da API anahtarını IConfiguration'dan alıyoruz
        public OpenAiService(IConfiguration configuration)
        {
            _apiKey = configuration["OpenAi:ApiKey"];

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("API Key is not set in appsettings.json.");
            }

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        // ChatGPT'ye mesaj gönderip yanıt alıyoruz
        public async Task<string> GetChatbotResponseAsync(string message)
        {
            try
            {
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "You are a helpful assistant." },
                        new { role = "user", content = message }
                    }
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<dynamic>(responseString);
                return responseJson.choices[0].message.content.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during API call: {ex.Message}");
                return "Sorry, I couldn't process yur request.";
            }
        }
    }
}
