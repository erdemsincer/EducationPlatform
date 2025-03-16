using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class OpenAiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAiService(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentNullException(nameof(apiKey), "OpenAI API key bulunamadı! Lütfen 'OPENAI_API_KEY' environment variable'ını ayarla.");
        }

        _httpClient = new HttpClient();
        _apiKey = apiKey;
    }



    public async Task<string> GetCareerAdvice(string skills, string interests, string careerGoals)
    {
        // **Verilerin boş olup olmadığını kontrol edelim**
        if (string.IsNullOrWhiteSpace(skills) || string.IsNullOrWhiteSpace(interests) || string.IsNullOrWhiteSpace(careerGoals))
        {
            return "Lütfen becerilerinizi, ilgi alanlarınızı ve kariyer hedeflerinizi eksiksiz girin.";
        }

        var requestBody = new
        {
            model = "gpt-4",
            messages = new[]
            {
            new { role = "system", content = "Sen bir kariyer danışmanı AI'sın. Kullanıcının becerileri, ilgi alanları ve kariyer hedeflerine göre önerilerde bulun." },
            new { role = "user", content = $"Benim becerilerim: {skills}. İlgi alanlarım: {interests}. Kariyer hedeflerim: {careerGoals}. Bana uygun meslekleri öner." }
        },
            temperature = 0.7
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

        if (!response.IsSuccessStatusCode)
        {
            return $"API isteği başarısız oldu. Hata kodu: {response.StatusCode}";
        }

        var responseString = await response.Content.ReadAsStringAsync();

        // **Gelen JSON verisini daha temiz bir formatta alalım**
        using var jsonDoc = JsonDocument.Parse(responseString);
        var root = jsonDoc.RootElement;
        var contentElement = root.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

        return contentElement ?? "Beklenmeyen bir hata oluştu.";
    }

}
