namespace Client.Pages
{
    public partial class Budget
    {
        private double income;
        private double needsAmount => Math.Round(income * 0.50, 2);
        private double wantsAmount => Math.Round(income * 0.30, 2);
        private double savingsAmount => Math.Round(income * 0.20, 2);

        private void CalculateBudget()
        {
            // This method is intentionally left empty.
            // The budget calculation is automatically triggered by the property getters
            // when 'income' is updated.
        }
    }
}
