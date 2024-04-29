using Client.Repositories.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Pages
{
    public partial class Budget
    {
        private double income = 1000;
        private bool IsValidIncome => income > 0;

        private double needsAmount => IsValidIncome ? Math.Round(income * 0.50, 2) : 0;
        private double wantsAmount => IsValidIncome ? Math.Round(income * 0.30, 2) : 0;
        private double savingsAmount => IsValidIncome ? Math.Round(income * 0.20, 2) : 0;

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ISettingsRepository SettingsRepository { get; set; }

        private string? UserId;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                UserId = user.FindFirst("sub")?.Value;

                try
                {
                    await FetchSettingsAsync();
                }
                catch (System.Exception)
                {
                    
                }
            }
        }

        private async Task FetchSettingsAsync()
        {
            var Settings = await SettingsRepository.GetSettings(UserId);

            if (!Settings.IsNull() && !Settings.Equals(null))
            {
                income = (double)Settings.MonthlyIncome;
            }
        }

        private void CalculateBudget()
        {
            // This method can contain any other logic needed when recalculating the budget.
            // Currently, it just checks for validity which is reflected in the getters.
        }
    }
}
