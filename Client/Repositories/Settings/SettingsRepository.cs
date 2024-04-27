using System.Net.Http.Json;
using Client.Repositories.Settings.Data;
using Newtonsoft.Json;

namespace Client.Repositories.Settings
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly IHttpClientFactory httpClientFactory;

        public SettingsRepository(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<UserSettings> GetSettings(string UserId)
        {
            var client = httpClientFactory.CreateClient("Settings");
            var dataResponse = await client.GetAsync($"Id/{UserId}");
            if (dataResponse.IsSuccessStatusCode)
            {
                var responseBody = await dataResponse.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(responseBody) || responseBody.Contains("\"value\": []"))
                {
                    throw new InvalidDataException(
                        $"User settings recieved was null for {UserId}."
                    );
                }
                else
                {
                    return JsonConvert.DeserializeObject<UserSettings>(responseBody);
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
            var dataResponse = await client.PatchAsync($"Id/{UserId}", JsonContent.Create(request));
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<UserSettings>(
                    await dataResponse.Content.ReadAsStringAsync());
                return dataResult;
            }
            else
            {
                throw new HttpRequestException(
                    $"Failed to retrieve settings for user {UserId}. HTTP status {dataResponse.StatusCode}"
                );

            }
        }
    }
}