﻿using API.Responses;
using Client.Repositories.Stock.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;

namespace Client.Repositories.Stock
{
    public class StockRepository : IStockRepository
    {
        private readonly IHttpClientFactory httpClientFactory;

        public StockRepository(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public virtual async Task<GetAnnualReturnsResponse> GetAnnualReturns(string symbol)
        {
            var client = httpClientFactory.CreateClient("Stock");
            var dataResponse = await client.GetAsync($"GetAnnualReturns?symbol={symbol}");
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<GetAnnualReturnsResponse>(
                    await dataResponse.Content.ReadAsStringAsync());
                return dataResult ?? new GetAnnualReturnsResponse();
            }
            else return new GetAnnualReturnsResponse();
        }

        public virtual async Task<GetStockMatchesResponse> GetStockMatches(string symbol)
        {
            var client = httpClientFactory.CreateClient("Stock");
            var dataResponse = await client.GetAsync($"GetStockMatches?symbol={symbol}");
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<GetStockMatchesResponse>(
                    await dataResponse.Content.ReadAsStringAsync());
                return dataResult ?? new GetStockMatchesResponse();
            }
            else return new GetStockMatchesResponse();
        }

        public virtual async Task<GetDailyHighsResponse> GetDailyHighs(string symbol)
        {
            var client = httpClientFactory.CreateClient("Stock");
            var dataResponse = await client.GetAsync($"GetDailyHighs?symbol={symbol}");
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<GetDailyHighsResponse>(
                    await dataResponse.Content.ReadAsStringAsync());
                return dataResult ?? new GetDailyHighsResponse();
            }
            else return new GetDailyHighsResponse();
        }

        public virtual async Task<GetMonthlyHighsResponse> GetMonthlyHighs(string symbol)
        {
            var client = httpClientFactory.CreateClient("Stock");
            var dataResponse = await client.GetAsync($"GetMonthlyHighs?symbol={symbol}");
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<GetMonthlyHighsResponse>(
                    await dataResponse.Content.ReadAsStringAsync());
                return dataResult ?? new GetMonthlyHighsResponse();
            }
            else return new GetMonthlyHighsResponse();
        }
    }
}
