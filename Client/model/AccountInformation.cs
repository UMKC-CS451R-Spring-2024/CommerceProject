namespace Client
{
    public partial class AccountInformation
    {

        public string AccountName { get; set; }

        public Accounts[] Accounts { get; set; }

        public int[] PastYearInvestments { get; set; }

    }

    public partial class Accounts
    {
        public string AccountType { get; set; }

        public int AccountAmount { get; set; }

        public int AccountNumber { get; set; }


    }

}
