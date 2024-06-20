using Newtonsoft.Json;

namespace FreeApi.Responses.ExchangeRates;

using System;
using System.ComponentModel.DataAnnotations;

public class CurrencyConversionResponseModel
{
    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("historical")]
    public string Historical { get; set; }

    [JsonProperty("info")]
    public CurrencyConversionInfo Info { get; set; }

    [JsonProperty("query")]
    public CurrencyConversionQuery Query { get; set; }

    [JsonProperty("result")]
    public decimal Result { get; set; }

    [JsonProperty("success")]
    public bool Success { get; set; }
}

public class CurrencyConversionInfo
{
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }
}

public class CurrencyConversionQuery
{
    [JsonProperty("amount")]
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }

    [JsonProperty("from")]
    [Required]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "From currency code must be exactly 3 characters")]
    public string From { get; set; }

    [JsonProperty("to")]
    [Required]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "To currency code must be exactly 3 characters")]
    public string To { get; set; }
}
