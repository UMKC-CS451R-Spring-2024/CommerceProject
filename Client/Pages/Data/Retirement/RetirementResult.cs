namespace Client.Pages.Data.Retirement
{
    public partial class Retirement
    {
        public class RetirementResult
        {
            public double RetirementValue { get; set; }
            public double ActualMonthlyIncome { get; set; }
            public double NeededAmount { get; set; }
            public double NeededContribution { get; set; }
            public List<double> ActualData { get; set; }
            public List<double> NeededData { get; set; }
        }
    }
}