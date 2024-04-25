using Bunit;
using Xunit;
using Client.Pages;

namespace Client.Tests.Pages {
    public class UserSettingsTests : TestContext {
       [Fact] 
       public void UserSettingsLoggedInRendersCorrectly() {
            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");

            var cut = RenderComponent<UserSettings>();

            cut.MarkupMatches("<p>Hello, TEST USER!</p>");
       }

       [Fact]
       public void UserSettingsLoggedOutRendersCorrectly() {
            var authContext = this.AddTestAuthorization();

            var cut = RenderComponent<UserSettings>();

            cut.MarkupMatches("<p>You're logged out.</p>");
       }
    }
}