using System.Text.Json.Serialization;

public class TestUserModel
{
    [JsonPropertyName("data")]
    public UserData Data { get; set; }

    [JsonPropertyName("support")]
    public Support Support { get; set; }
}

public class UserData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }
}

public class Support
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
}
