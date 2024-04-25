namespace Client.Repositories.Settings.Requests
{
    public class UpdateSettingsRequest
    {
        public int Age { get; set; }
        public int RetirementAge { get; set; }
        public double MonthlyIncome { get; set; }
        public double CurrentSavings { get; set; }
        public double MonthlyContributions { get; set; }
    }
}