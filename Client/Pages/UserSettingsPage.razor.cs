using Client.Repositories.Settings;
using Microsoft.AspNetCore.Components;
using Client.Repositories.Settings.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Data.Common;

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
                UserId = user.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(UserId))
                {
                    errorThrown = true;
                    errorText = "Unable to fetch user ID.";
                    return;
                }

                try
                {
                    await FetchOrInitializeSettingsAsync();
                    isLoaded = true;
                }
                catch (HttpRequestException)
                {
                    await InitializeDefaultSettingsAsync();
                    isLoaded = true;
                }
                catch (Exception e)
                {
                    errorThrown = true;
                    errorText = e.Message;
                }
            }
            else
            {
                Settings = new UserSettings();
                isLoaded = true;
            }
        }

        private async Task FetchOrInitializeSettingsAsync()
        {
            Settings = await SettingsRepository.GetSettings(UserId);

            if (Settings.IsNull())
            {
                await InitializeDefaultSettingsAsync();
            }
        }

        private async Task InitializeDefaultSettingsAsync()
        {
            Settings = new UserSettings();
            Settings.SetDefault(UserId);

            Settings = await SettingsRepository.CreateSettings(Settings);
        }
        protected async Task UpdateSettingsAsync(EditContext editContext)
        {
            if (editContext.Validate())
            {
                Settings.Id = null;
                await SettingsRepository.UpdateSettings(UserId, Settings);
            }
        }
    }
}