namespace Client.Pages
{
    public partial class Budget
    {
        private double income = 1000;
        private bool IsValidIncome => income > 0;

        private double needsAmount => IsValidIncome ? Math.Round(income * 0.50, 2) : 0;
        private double wantsAmount => IsValidIncome ? Math.Round(income * 0.30, 2) : 0;
        private double savingsAmount => IsValidIncome ? Math.Round(income * 0.20, 2) : 0;

        private void CalculateBudget()
        {
            // This method can contain any other logic needed when recalculating the budget.
            // Currently, it just checks for validity which is reflected in the getters.
        }
    }
}
