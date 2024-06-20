namespace FreeApi.Responses.ExchangeRates;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class HistoricalRateResponseModel
{
    /// <summary>
    /// The base currency for the exchange rates.
    /// </summary>
    [JsonPropertyName("base")]
    public string Base { get; set; }

    /// <summary>
    /// The date of the exchange rate data.
    /// </summary>
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Indicates whether the response contains historical exchange rates.
    /// </summary>
    [JsonPropertyName("historical")]
    public bool Historical { get; set; }

    /// <summary>
    /// A dictionary that maps currency codes to their corresponding exchange rates.
    /// </summary>
    [JsonPropertyName("rates")]
    public Dictionary<string, decimal> Rates { get; set; }

    /// <summary>
    /// Indicates whether the request was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// The timestamp of the exchange rate data.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }
}
