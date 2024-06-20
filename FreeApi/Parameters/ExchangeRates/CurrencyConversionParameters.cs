using System.ComponentModel.DataAnnotations;

namespace FreeApi.Parameters.ExchangeRates;

public class CurrencyConversionParameters
{
    [Required]
    [RegularExpression(@"^[0-9]+(\.[0-9]+)?$")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public double Amount { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "From currency code must be exactly 3 characters")]

    public string From { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "To currency code must be exactly 3 characters")]
    public string To { get; set; }

    [StringLength(10, ErrorMessage = "Date must be in YYYY-MM-DD format")]
    public DateTime Date { get; set; }

    [Required]
    public bool WithDate { get; set; }
}
