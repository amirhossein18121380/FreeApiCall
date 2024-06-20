namespace FreeApi.Parameters.ExchangeRates;

using System;
using System.ComponentModel.DataAnnotations;

public class HistoricalRateRequestParameters
{
    /// <summary>
    /// A date in the past for which historical rates are requested.
    /// </summary>
    [Required]
    public DateTime Date { get; set; }

    /// <summary>
    /// Enter the three-letter currency code of your preferred base currency.
    /// </summary>
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Base currency code must be exactly 3 characters long")]
    public string? Base { get; set; }

    /// <summary>
    /// Enter a list of comma-separated currency codes to limit output currencies.
    /// </summary>
    [StringLength(50, ErrorMessage = "Symbols must be less than or equal to 50 characters")]
    public string? Symbols { get; set; }
}
