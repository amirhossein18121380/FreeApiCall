namespace FreeApi.Responses.ExchangeRates;

using System.Text.Json.Serialization;

public class ExchangeRateResponseModel
{
    [JsonPropertyName("base")]
    public string BaseCurrency { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("rates")]
    public Dictionary<string, decimal> Rates { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }
}
