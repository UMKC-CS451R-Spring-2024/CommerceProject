using Newtonsoft.Json;


namespace Client
{
    public partial class AccountInformation
    {
        [JsonProperty("accountName")]
        public string AccountName { get; set; }


        [JsonProperty("accounts")]
        public Accounts[] Accounts { get; set; }

        [JsonProperty("pastYearInvestments")]
        public int[] PastYearInvestments { get; set; }


        [JsonProperty("financialAdvisorInfo")] 
        public string[] FinancialAdvisorInfo { get; set; }


    }

    public partial class Accounts
    {
        [JsonProperty("accountType")]
        public string AccountType { get; set; }

        [JsonProperty("accountAmount")]
        public long AccountAmount { get; set; }

        [JsonProperty("accountNumber")]
        public long AccountNumber { get; set; }
    }


}
