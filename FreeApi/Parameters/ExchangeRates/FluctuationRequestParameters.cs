using System.ComponentModel.DataAnnotations;

namespace FreeApi.Parameters.ExchangeRates;

public class FluctuationRequestParameters
{
    /// <summary>
    /// The end date of your preferred timeframe.
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// The start date of your preferred timeframe.
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// The three-letter currency code of your preferred base currency.
    /// </summary>
    [StringLength(3, MinimumLength = 3)]
    [RegularExpression(@"^[A-Z]{3}$")]
    public string? Base { get; set; }

    /// <summary>
    /// A list of comma-separated currency codes to limit output currencies.
    /// </summary>
    [RegularExpression(@"^([A-Z]{3}(,\s*[A-Z]{3})*)?$")]
    public string? Symbols { get; set; }

    public FluctuationRequestParameters()
    {
        // Set the maximum allowed timeframe to 365 days
        if (EndDate.Subtract(StartDate).Days > 365)
        {
            throw new ArgumentException("The maximum allowed timeframe is 365 days.");
        }
    }
}
