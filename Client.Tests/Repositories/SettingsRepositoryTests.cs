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
using System.Collections.Generic;

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
                Id = googleUserId,
                Age = 22,
                RetirementAge = 65,
                MonthlyIncome = 8000,
                CurrentSavings = 10000,
                MonthlyContributions = 500
            };
            var expectedUserSettingsWrapper = new UserSettingsWrapper()
            {
                UserSettingsList = []
            };
            expectedUserSettingsWrapper.UserSettingsList.Add(expectedUserSettings);
            var json = JsonConvert.SerializeObject(expectedUserSettingsWrapper);

            var MockHttpHandler = new MockHttpMessageHandler();
            var MockHttpClient = MockHttpHandler.ToHttpClient();
            MockHttpHandler.When($"https://localhost/Id/{googleUserId}").Respond("application/json", json);
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
        public async Task GetSettings_ReturnsNullUserOnNoUser()
        {
            var googleUserId = "0";
            UserSettings expectedUserSettings = new UserSettings();
            
            var json = "{\"value\": []}";

            var MockHttpHandler = new MockHttpMessageHandler();
            var MockHttpClient = MockHttpHandler.ToHttpClient();
            MockHttpHandler.When($"https://localhost/Id/{googleUserId}").Respond("application/json", json);
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
        public async Task GetSettings_ThrowsHttpRequestException()
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

        [Fact]
        public async Task UpdateSettings_ReturnsExpectedSettings()
        {
            var googleUserId = "0";
            var expectedUserSettings = new UserSettings
            {
                Id = googleUserId,
                Age = 22,
                RetirementAge = 65,
                MonthlyIncome = 8000,
                CurrentSavings = 10000,
                MonthlyContributions = 500
            };
            var expectedUserSettingsWrapper = new UserSettingsWrapper()
            {
                UserSettingsList = []
            };
            expectedUserSettingsWrapper.UserSettingsList.Add(expectedUserSettings);
            var json = JsonConvert.SerializeObject(expectedUserSettingsWrapper);

            var MockHttpHandler = new MockHttpMessageHandler();
            var MockHttpClient = MockHttpHandler.ToHttpClient();
            MockHttpHandler.When($"https://localhost/Id/{googleUserId}").Respond("application/json", json);
            MockHttpClient.BaseAddress = new Uri("https://localhost");

            var httpClientFactory = Mock.Of<IHttpClientFactory>(f => f.CreateClient("Settings") == MockHttpClient);

            var SettingsRepository = new SettingsRepository(httpClientFactory);
            var result = await SettingsRepository.UpdateSettings(googleUserId, expectedUserSettings);

            Assert.Equal(expectedUserSettings.Id, result.Id);
            Assert.Equal(expectedUserSettings.Age, result.Age);
            Assert.Equal(expectedUserSettings.RetirementAge, result.RetirementAge);
            Assert.Equal(expectedUserSettings.MonthlyIncome, result.MonthlyIncome);
            Assert.Equal(expectedUserSettings.CurrentSavings, result.CurrentSavings);
            Assert.Equal(expectedUserSettings.MonthlyContributions, result.MonthlyContributions);
        }

        [Fact]
        public async Task UpdateSettings_ThrowsHttpRequestException()
        {
            var googleUserId = "0";

            var MockHttpHandler = new MockHttpMessageHandler();
            var MockHttpClient = MockHttpHandler.ToHttpClient();
            MockHttpHandler.When($"https://localhost/Id/{googleUserId}").Respond(HttpStatusCode.BadRequest);
            MockHttpClient.BaseAddress = new Uri("https://localhost");

            var httpClientFactory = Mock.Of<IHttpClientFactory>(f => f.CreateClient("Settings") == MockHttpClient);

            var SettingsRepository = new SettingsRepository(httpClientFactory);
            await Assert.ThrowsAsync<HttpRequestException>(() => SettingsRepository.UpdateSettings(googleUserId, new UserSettings()));
        }

        [Fact]
        public async Task CreateSettings_ReturnsExpectedSettings()
        {
            var googleUserId = "0";
            var expectedUserSettings = new UserSettings
            {
                Id = googleUserId,
                Age = 22,
                RetirementAge = 65,
                MonthlyIncome = 8000,
                CurrentSavings = 10000,
                MonthlyContributions = 500
            };
            var expectedUserSettingsWrapper = new UserSettingsWrapper()
            {
                UserSettingsList = []
            };
            expectedUserSettingsWrapper.UserSettingsList.Add(expectedUserSettings);
            var json = JsonConvert.SerializeObject(expectedUserSettingsWrapper);

            var MockHttpHandler = new MockHttpMessageHandler();
            var MockHttpClient = MockHttpHandler.ToHttpClient();
            MockHttpHandler.When($"https://localhost/").Respond("application/json", json);
            MockHttpClient.BaseAddress = new Uri("https://localhost");

            var httpClientFactory = Mock.Of<IHttpClientFactory>(f => f.CreateClient("Settings") == MockHttpClient);

            var SettingsRepository = new SettingsRepository(httpClientFactory);
            var result = await SettingsRepository.CreateSettings(expectedUserSettings);

            Assert.Equal(expectedUserSettings.Id, result.Id);
            Assert.Equal(expectedUserSettings.Age, result.Age);
            Assert.Equal(expectedUserSettings.RetirementAge, result.RetirementAge);
            Assert.Equal(expectedUserSettings.MonthlyIncome, result.MonthlyIncome);
            Assert.Equal(expectedUserSettings.CurrentSavings, result.CurrentSavings);
            Assert.Equal(expectedUserSettings.MonthlyContributions, result.MonthlyContributions);
        }

        [Fact]
        public async Task CreateSettings_ThrowsHttpRequestException()
        {
            var googleUserId = "0";

            var MockHttpHandler = new MockHttpMessageHandler();
            var MockHttpClient = MockHttpHandler.ToHttpClient();
            MockHttpHandler.When($"https://localhost/").Respond(HttpStatusCode.BadRequest);
            MockHttpClient.BaseAddress = new Uri("https://localhost");

            var httpClientFactory = Mock.Of<IHttpClientFactory>(f => f.CreateClient("Settings") == MockHttpClient);

            var SettingsRepository = new SettingsRepository(httpClientFactory);
            await Assert.ThrowsAsync<HttpRequestException>(() => SettingsRepository.CreateSettings(new UserSettings()));
        }
    }
}