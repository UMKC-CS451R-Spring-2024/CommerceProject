using Moq;
using Client.Repositories.Settings;
using System.Threading.Tasks;
using Client.Repositories.Settings.Data;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Client.Tests.Utils;
using System;
using System.IO;

namespace Client.Tests.Repositories
{
    public class SettingsRepositoryTests
    {
        [Fact]
        public async Task GetSettings_ReturnsExpectedSettings()
        {
            var googleUserId = "0";
            var expectedUserSettings = new UserSettings
            {
                Age = 22,
                RetirementAge = 65,
                MonthlyIncome = 8000,
                CurrentSavings = 10000,
                MonthlyContributions = 500
            };

            var MockHttpHandler = new MockHttpMessageHandler();
            var MockHttpClient = MockHttpHandler.ToHttpClient();
            MockHttpHandler.When($"https://localhost/Id/{googleUserId}").RespondJson(expectedUserSettings);
            MockHttpClient.BaseAddress = new Uri("https://localhost");

            var httpClientFactory = Mock.Of<IHttpClientFactory>(f => f.CreateClient("Settings") == MockHttpClient);

            var SettingsRepository = new SettingsRepository(httpClientFactory);
            var result = await SettingsRepository.GetSettings(googleUserId);

            Assert.Equal(expectedUserSettings.Age, result.Age);
            Assert.Equal(expectedUserSettings.RetirementAge, result.RetirementAge);
            Assert.Equal(expectedUserSettings.MonthlyIncome, result.MonthlyIncome);
            Assert.Equal(expectedUserSettings.CurrentSavings, result.CurrentSavings);
            Assert.Equal(expectedUserSettings.MonthlyContributions, result.MonthlyContributions);
        }

        [Fact]
        public async Task GetSettings_ThrowsInvalidDataException()
        {
            var googleUserId = "0";
            UserSettings expectedUserSettings = null;

            var MockHttpHandler = new MockHttpMessageHandler();
            var MockHttpClient = MockHttpHandler.ToHttpClient();
            MockHttpHandler.When($"https://localhost/Id/{googleUserId}").RespondJson(expectedUserSettings);
            MockHttpClient.BaseAddress = new Uri("https://localhost");

            var httpClientFactory = Mock.Of<IHttpClientFactory>(f => f.CreateClient("Settings") == MockHttpClient);

            var SettingsRepository = new SettingsRepository(httpClientFactory);
            await Assert.ThrowsAsync<InvalidDataException>(() => SettingsRepository.GetSettings(googleUserId));
        }

        [Fact]
        public async Task GetSettings_ThrowsIHttpRequestException()
        {
            var googleUserId = "0";

            var MockHttpHandler = new MockHttpMessageHandler();
            var MockHttpClient = MockHttpHandler.ToHttpClient();
            MockHttpHandler.When($"https://localhost/Id/{googleUserId}").Respond(HttpStatusCode.BadRequest);
            MockHttpClient.BaseAddress = new Uri("https://localhost");

            var httpClientFactory = Mock.Of<IHttpClientFactory>(f => f.CreateClient("Settings") == MockHttpClient);

            var SettingsRepository = new SettingsRepository(httpClientFactory);
            await Assert.ThrowsAsync<HttpRequestException>(() => SettingsRepository.GetSettings(googleUserId));
        }
    }
}