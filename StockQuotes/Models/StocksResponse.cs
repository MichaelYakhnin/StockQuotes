using Newtonsoft.Json;

namespace StockQuotes.Models
{

    internal class StocksResponse
    {

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }
    }

}
