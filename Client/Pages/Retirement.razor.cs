using Client.Repositories.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using static Client.Pages.Data.Retirement.Retirement;

namespace Client.Pages
{
    public partial class Retirement
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ISettingsRepository SettingsRepository { get; set; }

        private string? UserId;
        private RetirementData data = new RetirementData();
        private RetirementResult result;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            data = new RetirementData();

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
                data.Age = (int)Settings.Age;
                data.RetirementAge = (int)Settings.RetirementAge;
                data.MonthlyIncome = (double)Settings.MonthlyIncome;
                data.CurrentSavings = (double)Settings.CurrentSavings;
                data.MonthlyContribution = (double)Settings.MonthlyContributions;
            }
        }

        private List<double> GetData(double payment)
        {
            List<double> dataset = new List<double>
            {
            };
            for (int i = data.Age; i <= data.RetirementAge; i++)
            {
                dataset.Add(CalculateRetirementValue(i - data.Age, data.ReturnRate, data.InflationRate, data.CurrentSavings, payment));
            }

            double retirementAmount = dataset.Last();
            for (int i = data.RetirementAge + 1; i < data.LifeExpectancy; i++)
            {
                dataset.Add(Math.Max(0, CalculateRetirementValue(i - data.RetirementAge, data.RetirementReturnRate, data.InflationRate, retirementAmount, -data.MonthlyBudget)));
            }

            return dataset;
        }

        public void CalculateRetirement()
        {
            result = new RetirementResult();
            result.RetirementValue = CalculateRetirementValue((data.RetirementAge - data.Age + 1), data.ReturnRate, data.InflationRate, data.CurrentSavings, data.MonthlyContribution);
            result.ActualMonthlyIncome = CalculateAmortization((data.LifeExpectancy - data.RetirementAge + 1), data.RetirementReturnRate, data.InflationRate, result.RetirementValue);
            result.NeededAmount = CalculateNeededAmount((data.LifeExpectancy - data.RetirementAge + 1), data.RetirementReturnRate, data.InflationRate, data.MonthlyBudget);
            result.NeededContribution = CalculateNeededContribution((data.RetirementAge - data.Age + 1), data.ReturnRate, data.InflationRate, data.CurrentSavings, result.NeededAmount);
            result.ActualData = GetData(data.MonthlyContribution);
            result.NeededData = GetData(result.NeededContribution);
        }

        private double CalculateAmortization(int years, double returnRate, double inflation, double principal)
        {
            double rate = (returnRate - inflation) / 1200;
            int periods = years * 12;
            double withdrawal = principal * rate / ((1 - (1 / Math.Pow(1 + rate, periods))) * (1 + rate));
            return withdrawal;
        }

        private double CalculateNeededContribution(int years, double returnRate, double inflation, double principal, double neededAmount)
        {
            double rate = (returnRate - inflation) / 1200;
            int periods = years * 12;
            double contribution = rate * (neededAmount - (principal * Math.Pow(1 + rate, periods))) / (Math.Pow(1 + rate, periods) - 1);
            return contribution;
        }

        private double CalculateRetirementValue(int years, double returnRate, double inflation, double principal, double payments)
        {
            double rate = (returnRate - inflation) / 1200;
            int periods = years * 12;
            double contributionValue = (payments / rate) * (Math.Pow(1 + rate, periods) - 1);
            double principalValue = principal * Math.Pow(1 + rate, periods);
            return contributionValue + principalValue;
        }

        private double CalculateNeededAmount(int years, double returnRate, double inflation, double income)
        {
            double rate = (returnRate - inflation) / 1200;
            int periods = years * 12;
            double amount = (income * (1 - (1 / Math.Pow(1 + rate, periods)))) / rate;
            return amount;
        }
    }
}