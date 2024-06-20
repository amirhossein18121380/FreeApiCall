using System.Text.Json;
using System.Text.Json.Serialization;


namespace FreeApi.Responses.ExchangeRates;

public class FluctuationResponseModel
{
    [JsonPropertyName("base")]
    public string Base { get; set; }

    [JsonPropertyName("end_date")]
    public string EndDate { get; set; }

    [JsonPropertyName("fluctuation")]
    public bool Fluctuation { get; set; }

    [JsonPropertyName("rates")]
    public Dictionary<string, CurrencyFluctuation> Rates { get; set; }

    [JsonPropertyName("start_date")]
    public string StartDate { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }
}


public class CurrencyFluctuation
{
    [JsonPropertyName("change")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal Change { get; set; }

    [JsonPropertyName("change_pct")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal ChangePct { get; set; }

    [JsonPropertyName("end_rate")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal EndRate { get; set; }

    [JsonPropertyName("start_rate")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal StartRate { get; set; }
}


public class DecimalJsonConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                var stringValue = reader.GetString();
                if (decimal.TryParse(stringValue, out var decimalValue))
                {
                    return decimalValue;
                }
                if (stringValue.Equals("unavailable", StringComparison.OrdinalIgnoreCase))
                {
                    return 0m; // or throw an exception, or handle it in a way suitable for your application
                }
                throw new JsonException($"Unable to convert \"{stringValue}\" to {typeToConvert}.");

            case JsonTokenType.Number:
                if (reader.TryGetDecimal(out decimal numberValue))
                {
                    return numberValue;
                }
                break;

            default:
                throw new JsonException($"Unexpected token parsing {typeToConvert}. Token: {reader.TokenType}");
        }

        throw new JsonException($"Unable to convert {reader.TokenType} to {typeToConvert}");
    }


    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}