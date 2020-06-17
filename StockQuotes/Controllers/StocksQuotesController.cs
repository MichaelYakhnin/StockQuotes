using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StockQuotes.Models;

namespace StockQuotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksQuotesController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiKey = "788ffbf057dad1fb71efeb1899276ff0";

        public StocksQuotesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("quotes")]
        public async Task<IActionResult> GetQuotes([FromQuery] string symbol)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://financialmodelingprep.com/api/v3/historical-chart/1hour/" + symbol + "?apikey=" + ApiKey);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "Stocks");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var quoteResponse = JsonConvert.DeserializeObject<IEnumerable<QuoteResponse>>(responseStream);
                var result = Parser(quoteResponse);
                return Ok(result);
            }
            else
            {
                return Ok(new { error  = true});
            }
        }

        [HttpGet("stocks")]
        public async Task<IActionResult> GetStocks()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://financialmodelingprep.com/api/v3/stock/list?apikey=" + ApiKey);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "Stocks");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var stockResponse =  JsonConvert.DeserializeObject<List<StocksResponse>>(responseStream);
                return Ok(stockResponse);
            }
            else
            {
                return Ok(new { error = true });
            }
        }

        private double[][] Parser(IEnumerable<QuoteResponse> quoteResponses)
        {
            var result = new List<double[]>();
            foreach (var item in quoteResponses)
            {
                double[] ohlc = { ((DateTimeOffset)DateTime.Parse(item.Date)).ToUnixTimeMilliseconds(), item.Open, item.High, item.Low, item.Close };
                result.Add(ohlc);
            }
            return result.ToArray();
        }
    }
}
