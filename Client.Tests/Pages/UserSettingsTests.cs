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
          public void LoggedInRendersCorrectly()
          {
               int googleUserId = 0;
               string UserName = "TEST USER";
               var authContext = this.AddTestAuthorization();
               authContext.SetAuthorized(UserName);
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
               heading.MarkupMatches($@"<div id=""heading"">
                                        <p>Hello, {UserName}!</p>
                                        <h1>Your settings:</h1>
                                   </div>");
          }

          [Fact]
          public void LoggedOutRendersCorrectly()
          {
               var authContext = this.AddTestAuthorization();
               Mock<ISettingsRepository> SettingsRepositoryMock = new Mock<ISettingsRepository>();
               Services.AddSingleton<ISettingsRepository>(SettingsRepositoryMock.Object);


               var cut = RenderComponent<UserSettingsPage>();

               cut.MarkupMatches(@"<h1>You're logged out.<a href=""authentication/login"">Log in</a></h1>");
          }

          [Fact]
          public void FormRendersCorrectly()
          {
               int googleUserId = 0;
               string UserName = "TEST USER";
               var expectedUser = new UserSettings
               {
                    Age = 22,
                    RetirementAge = 65,
                    MonthlyIncome = 8000,
                    CurrentSavings = 10000,
                    MonthlyContributions = 800
               };
               var authContext = this.AddTestAuthorization();
               authContext.SetAuthorized(UserName);
               authContext.SetClaims(new Claim("sub", googleUserId.ToString()));

               Mock<ISettingsRepository> SettingsRepositoryMock = new Mock<ISettingsRepository>();
               SettingsRepositoryMock
                    .Setup(s => s.GetSettings(googleUserId.ToString()))
                    .ReturnsAsync(expectedUser);
               Services.AddSingleton<ISettingsRepository>(SettingsRepositoryMock.Object);

               var cut = RenderComponent<UserSettingsPage>();
               var heading = cut.Find("#settingsInput");
               heading.MarkupMatches($@"<div id=""settingsInput"">
                                        <form>
                                             <div class=""d-flex flex-column"">
                                                  <div class=""form-group"">
                                                       <label for=""age"">Age:</label>
                                                       <input step=""any"" id=""age"" type=""number"" name=""Settings.Age"" class=""form-control valid"" value=""{expectedUser.Age}"">
                                                  </div>

                                                  <div class=""form-group"">
                                                       <label for=""retirementAge"">Retirement Age:</label>
                                                       <input step=""any"" id=""retirementAge"" type=""number"" name=""Settings.RetirementAge"" class=""form-control valid"" value=""{expectedUser.RetirementAge}"">
                                                  </div>

                                                  <div class=""form-group"">
                                                       <label for=""income"">Monthly Income:</label>
                                                       <input step=""any"" id=""income"" type=""number"" name=""Settings.MonthlyIncome"" class=""form-control valid"" value=""{expectedUser.MonthlyIncome}"">
                                                  </div>

                                                  <div class=""form-group"">
                                                       <label for=""savings"">Savings:</label>
                                                       <input step=""any"" id=""savings"" type=""number"" name=""Settings.CurrentSavings"" class=""form-control valid"" value=""{expectedUser.CurrentSavings}"">
                                                  </div>

                                                  <div class=""form-group"">
                                                       <label for=""contributions"">Monthly Contributions:</label>
                                                       <input step=""any"" id=""contributions"" type=""number"" name=""Settings.MonthlyContributions"" class=""form-control valid"" value=""{expectedUser.MonthlyContributions}"">
                                                  </div>

                                                  <button type=""submit"" class=""btn btn-primary"">Update Settings</button>
                                             </div>
                                        </form>
                                   </div>");
          }

          [Fact]
          public void ErrorRendersCorrectly()
          {
               int googleUserId = 0;
               string UserName = "TEST USER";
               var authContext = this.AddTestAuthorization();
               authContext.SetAuthorized(UserName);
               authContext.SetClaims(new Claim("sub", googleUserId.ToString()));

               Mock<ISettingsRepository> SettingsRepositoryMock = new Mock<ISettingsRepository>();
               Services.AddSingleton<ISettingsRepository>(SettingsRepositoryMock.Object);

               var cut = RenderComponent<UserSettingsPage>();
               var heading = cut.Find("#settingsInput");
               heading.MarkupMatches(@"<div id=""settingsInput"">
                                        <p>Error in fetching data.</p>
                                   </div>");
          }
     }
}