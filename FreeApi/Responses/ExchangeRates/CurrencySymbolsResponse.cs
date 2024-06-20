namespace FreeApi.Responses.ExchangeRates;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class CurrencySymbolsResponse
{
    /// <summary>
    /// Indicates whether the request was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// A dictionary that maps currency codes to their corresponding names.
    /// </summary>
    [JsonPropertyName("symbols")]
    public Dictionary<string, string> Symbols { get; set; }
}
