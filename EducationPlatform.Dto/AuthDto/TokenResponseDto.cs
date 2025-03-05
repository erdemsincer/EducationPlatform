using Newtonsoft.Json;

public class TokenResponseDto
{
    [JsonProperty("token")]  // API JSON'daki "token" ile eşleşiyor
    public string AccessToken { get; set; }
    public List<string> Roles { get; set; }


}
