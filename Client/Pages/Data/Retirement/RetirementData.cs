using System.ComponentModel.DataAnnotations;

namespace Client.Pages.Data.Retirement
{
    public partial class Retirement
    {
        public class RetirementData
        {
            [Required(ErrorMessage = "Current age is required")]
            [Range(18, 120, ErrorMessage = "Enter an age between 18 and 120")]
            public int Age { get; set; } = 22;

            [Required(ErrorMessage = "Current savings are required")]
            [Range(0, 100000000, ErrorMessage = "Enter current savings between 0 and 100000000")]
            public double CurrentSavings { get; set; } = 5000;

            [Required(ErrorMessage = "Monthly income is required")]
            [Range(0, 1000000, ErrorMessage = "Enter a monthly income between 0 and 1000000")]
            public double MonthlyIncome { get; set; } = 2500;

            [Required(ErrorMessage = "Monthly contribution is required")]
            [Range(0, 10000000, ErrorMessage = "Enter a monthly contribution between 0 and 10000000")]
            public double MonthlyContribution { get; set; } = 250;

            [Required(ErrorMessage = "Monthly retirement budget is required")]
            [Range(0, 10000000, ErrorMessage = "Enter a monthly retirement budget between 0 and 10000000")]
            public double MonthlyBudget { get; set; } = 2500;

            [Required]
            [Range(18, 120, ErrorMessage = "Enter an age between 18 and 120")]
            public int RetirementAge { get; set; } = 65;

            [Required]
            [Range(18, 120, ErrorMessage = "Enter an age between 18 and 120")]
            public int LifeExpectancy { get; set; } = 100;

            [Required]
            [Range(0, 15, ErrorMessage = "Enter a return rate between 0% and 15%")]
            public double ReturnRate { get; set; } = 10;

            [Required]
            [Range(0, 15, ErrorMessage = "Enter a return rate between 0% and 15%")]
            public double RetirementReturnRate { get; set; } = 5;

            [Required]
            [Range(0, 15, ErrorMessage = "Enter an inflation rate between 0% and 15%")]
            public double InflationRate { get; set; } = 3;
            // [Required]
            // [Range(0, 15, ErrorMessage = "Enter an income increase rate between 0% and 15%")]
            // public double IncomeIncreaseRate { get; set; } = 2;
        }
    }
}