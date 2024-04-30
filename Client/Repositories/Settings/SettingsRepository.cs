using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Client.Repositories.Settings.Data;
using Newtonsoft.Json;

namespace Client.Repositories.Settings
{
    public class UserSettingsWrapper
    {
        [JsonProperty("value")]
        public List<UserSettings> UserSettingsList { get; set; }
    }

    public class SettingsRepository : ISettingsRepository
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly JsonSerializerOptions jsonOptions;

        public SettingsRepository(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            jsonOptions.PropertyNamingPolicy = null;
        }

        public async Task<UserSettings> GetSettings(string UserId)
        {
            var client = httpClientFactory.CreateClient("Settings");
            var dataResponse = await client.GetAsync($"Id/{UserId}");
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<UserSettingsWrapper>(
                    await dataResponse.Content.ReadAsStringAsync());
                var json = dataResponse.Content.ReadAsStringAsync();
                if (dataResult != null && dataResult.UserSettingsList.Any())
                {
                    return dataResult.UserSettingsList.First();
                }
                else
                {
                    return new UserSettings();
                }
            }
            else
            {
                throw new HttpRequestException(
                    $"Failed to retrieve settings for user {UserId}. HTTP status {dataResponse.StatusCode}"
                );

            }
        }

        public async Task<UserSettings> UpdateSettings(string UserId, UserSettings request)
        {
            var client = httpClientFactory.CreateClient("Settings");
            var dataResponse = await client.PutAsJsonAsync($"Id/{UserId}", request, jsonOptions);
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<UserSettingsWrapper>(
                    await dataResponse.Content.ReadAsStringAsync());
                return dataResult.UserSettingsList.First();
            }
            else
            {
                throw new HttpRequestException(
                    $"Failed to retrieve settings for user {UserId}. HTTP status {dataResponse.StatusCode}. {await dataResponse.Content.ReadAsStringAsync()}"
                );

            }
        }

        public async Task<UserSettings> CreateSettings(UserSettings request)
        {
            var client = httpClientFactory.CreateClient("Settings");

            var dataResponse = await client.PostAsJsonAsync("", request, jsonOptions);
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<UserSettingsWrapper>(
                    await dataResponse.Content.ReadAsStringAsync());
                return dataResult.UserSettingsList.First();
            }
            else
            {
                throw new HttpRequestException(
                    $"Failed to create settings. HTTP status {dataResponse.StatusCode}. {await dataResponse.Content.ReadAsStringAsync()}"
                );

            }
        }
    }
}