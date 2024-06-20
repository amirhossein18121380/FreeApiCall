namespace FreeApi.Responses.ExchangeRates;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class GetTimeSeriesResponseModel
{
    /// <summary>
    /// The base currency for the exchange rates.
    /// </summary>
    [JsonPropertyName("base")]
    public string Base { get; set; }

    /// <summary>
    /// The end date of the requested timeframe.
    /// </summary>
    [JsonPropertyName("end_date")]
    public string EndDate { get; set; }

    /// <summary>
    /// A dictionary that maps dates to a dictionary of exchange rates for different currencies.
    /// </summary>
    [JsonPropertyName("rates")]
    public Dictionary<string, Dictionary<string, decimal>> Rates { get; set; }

    /// <summary>
    /// The start date of the requested timeframe.
    /// </summary>
    [JsonPropertyName("start_date")]
    public string StartDate { get; set; }

    /// <summary>
    /// Indicates whether the request was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Indicates whether the response contains a timeseries of exchange rates.
    /// </summary>
    [JsonPropertyName("timeseries")]
    public bool Timeseries { get; set; }
}
