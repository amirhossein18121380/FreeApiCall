using System.ComponentModel.DataAnnotations;

namespace FreeApi.Parameters.ExchangeRates;



public class GetTimeSeriesRequestParameters
{
    /// <summary>
    /// The start date of your preferred timeframe.
    /// </summary>
    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid date format. Use YYYY-MM-DD.")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// The end date of your preferred timeframe.
    /// </summary>
    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid date format. Use YYYY-MM-DD.")]
    public DateTime EndDate { get; set; }


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

    public bool IsValid()
    {
        if (StartDate > EndDate)
        {
            throw new ArgumentException("The start date must be less than the end date.");
        }
        if (EndDate - StartDate > TimeSpan.FromDays(365))
        {
            throw new ArgumentException("The date range must be less than 365 days.");
        }
        return true;
    }
}
