using Client.Repositories.Settings.Data;
using Newtonsoft.Json;

namespace Client.Repositories.Settings
{
    public class SettingsRepository: ISettingsRepository
    {
        private readonly IHttpClientFactory httpClientFactory;

        public SettingsRepository(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<UserSettings> GetSettings(string UserId)
        {
            var client = httpClientFactory.CreateClient("Settings");
            var dataResponse = await client.GetAsync($"Settings/Id/{UserId}");
            if (dataResponse.IsSuccessStatusCode) {
                var dataResult = JsonConvert.DeserializeObject<UserSettings>(
                    await dataResponse.Content.ReadAsStringAsync());
                return dataResult ?? new UserSettings();
            }
            else return new UserSettings();
        }

        public Task<UserSettings> UpdateSettings(string UserId, UserSettings request)
        {
            throw new NotImplementedException();
        }
    }
}