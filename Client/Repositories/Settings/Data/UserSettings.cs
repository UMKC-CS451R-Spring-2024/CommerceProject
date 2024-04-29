using Newtonsoft.Json;

namespace Client.Repositories.Settings.Data
{
    public class UserSettings
    {
        public string? Id { get; set; }
        public int? Age { get; set; }
        public int? RetirementAge { get; set; }
        public double? MonthlyIncome { get; set; }
        public double? CurrentSavings { get; set; }
        
        public double? MonthlyContributions { get; set; }

        public bool IsNull() {
            return !(Age.HasValue || RetirementAge.HasValue || MonthlyIncome.HasValue || CurrentSavings.HasValue || MonthlyContributions.HasValue);
        }

        public void SetDefault(string id) {
            Id = id;
            Age = 21;
            RetirementAge = 65;
            MonthlyIncome = 8000;
            CurrentSavings = 1000;
            MonthlyContributions = 800;
        }
    }
}