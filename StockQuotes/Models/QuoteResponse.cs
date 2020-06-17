using Newtonsoft.Json;

namespace StockQuotes.Models
{

    public class QuoteResponse
    {

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("volume")]
        public int Volume { get; set; }
    }

}
