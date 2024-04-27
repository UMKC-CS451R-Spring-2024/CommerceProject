using Client.Repositories.Settings;
using Microsoft.AspNetCore.Components;
using Client.Repositories.Settings.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Pages
{
    public partial class UserSettingsPage
    {
        [Inject]
        public ISettingsRepository SettingsRepository { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected string UserId { get; private set; }

        protected UserSettings Settings { get; set; }

        protected bool isLoaded { get; set; }
        protected bool errorThrown { get; set; }
        protected string errorText { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                isLoaded = false;
                errorThrown = false;
                errorText = string.Empty;
                UserId = user.FindFirst("sub")?.Value;

                try
                {
                    Settings = await SettingsRepository.GetSettings(UserId);
                    isLoaded = true;
                }
                catch (Exception ex) when (ex is InvalidDataException || ex is HttpRequestException)
                {
                    errorThrown = true;
                    errorText = ex.Message;
                }
            }
            else
            {
                Settings = new UserSettings();
                isLoaded = true;
            }
        }

        protected async Task UpdateSettingsAsync(EditContext editContext)
        {
            if (editContext.Validate())
            {
                await SettingsRepository.UpdateSettings(UserId, Settings);
            }
        }
    }
}