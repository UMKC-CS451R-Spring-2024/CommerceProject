using Bunit;
using Xunit;
using Client.Pages;
using System.Security.Claims;
using Client.Repositories.Settings;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Client.Repositories.Settings.Data;

namespace Client.Tests.Pages
{
     public class UserSettingsTests : TestContext
     {
          [Fact]
          public void UserSettingsLoggedInRendersCorrectly()
          {
               int googleUserId = 0;
               var authContext = this.AddTestAuthorization();
               authContext.SetAuthorized("TEST USER");
               authContext.SetClaims(new Claim("sub", googleUserId.ToString()));

               Mock<ISettingsRepository> SettingsRepositoryMock = new Mock<ISettingsRepository>();
               // SettingsRepositoryMock
               //      .Setup(s => s.GetSettings(googleUserId.ToString()))
               //      .ReturnsAsync(new UserSettings
               //      {
               //           Age = 22,
               //           RetirementAge = 65,
               //           MonthlyIncome = 8000,
               //           CurrentSavings = 10000,
               //           MonthlyContributions = 800
               //      });
               Services.AddSingleton<ISettingsRepository>(SettingsRepositoryMock.Object);

               var cut = RenderComponent<UserSettingsPage>();
               var heading = cut.Find("#heading");
               heading.MarkupMatches(@"<div id=""heading"">
                                        <p>Hello, TEST USER!</p>
                                        <h1>Your settings:</h1>
                                   </div>");
          }

          [Fact]
          public void UserSettingsLoggedOutRendersCorrectly()
          {
               var authContext = this.AddTestAuthorization();
               Mock<ISettingsRepository> SettingsRepositoryMock = new Mock<ISettingsRepository>();
               Services.AddSingleton<ISettingsRepository>(SettingsRepositoryMock.Object);


               var cut = RenderComponent<UserSettingsPage>();

               cut.MarkupMatches("<h1>You're logged out.</h1>");
          }
     }
}