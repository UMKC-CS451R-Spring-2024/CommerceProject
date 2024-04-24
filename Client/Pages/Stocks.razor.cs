using Newtonsoft.Json;

namespace Client.Pages
{
    public partial class Stocks
    {
        string apiKey = "KRY4J4J7NUX3OZG8";
        string searchQuery = "";
        string url = "https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords=tesco&apikey=demo";
        private StockSearchInfo? searchResults;
        public List<StockSearchInfo> listOfSearchResults = new List<StockSearchInfo>();
        public async void getDailyStockData()
        {
            if (!searchQuery.Equals(""))
            {
                url = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={searchQuery}&apikey={apiKey}";
                using (HttpClient http = new HttpClient())
                {
                    HttpResponseMessage response = await http.GetAsync(url);
                    // Ensure the response was successful
                    response.EnsureSuccessStatusCode();
                    // Read the content of the response as a string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Print the JSON data
                    Console.WriteLine(responseBody);
                    searchResults = JsonConvert.DeserializeObject<StockSearchInfo>(responseBody);
                    Console.WriteLine(searchResults.BestMatches[0].Symbol);
                }
            }
        }
    }
}