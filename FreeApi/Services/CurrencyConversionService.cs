using FreeApi.Parameters.ExchangeRates;
using FreeApi.RequestHelper;
using FreeApi.Responses.ExchangeRates;
using System.Globalization;

namespace FreeApi.Services;

public interface ICurrencyConversionService
{
    Task<CurrencyConversionResponseModel> ConvertCurrencyAsync(CurrencyConversionParameters parameter);
    Task<FluctuationResponseModel> GetFluctuationAsync(FluctuationRequestParameters parameter);
    Task<ExchangeRateResponseModel> GetLatestRateAsync(ExchangeRateRequestParameters parameter);
    Task<CurrencySymbolsResponse> GetSymbolListAsync();
    Task<GetTimeSeriesResponseModel> GetTimeSeriesAsync(GetTimeSeriesRequestParameters parameter);
    Task<HistoricalRateResponseModel> GetHistoricalRatesAsync(HistoricalRateRequestParameters parameter);
}

public class CurrencyConversionService(HttpClient httpClient) : ApiClientBase, ICurrencyConversionService
{
    private const string ApiKey = "U2RuA0G9Nwr4lyOmlBXsfzpjVHBak9iz";
    private const string FixUrlSection = "/exchangerates_data";

    /// <summary>
    /// Converts currency using the provided parameters.
    /// </summary>
    /// <param name="parameter">The currency conversion parameters.</param>
    /// <returns>A <see cref="CurrencyConversionResponseModel"/> containing the converted currency.</returns>
    ///<example>
    /// "https://api.apilayer.com/exchangerates_data/convert?to={to}&amp;from={from}&amp;amount={amount}"
    /// </example>
    public async Task<CurrencyConversionResponseModel> ConvertCurrencyAsync(CurrencyConversionParameters parameter)
    {
        var builder = httpClient
            .UsingRoute($"{FixUrlSection}/convert")
            .WithQueryParam("to", parameter.To)
            .WithQueryParam("from", parameter.From)
            .WithQueryParam("amount", parameter.Amount.ToString());

        if (parameter.WithDate)
        {
            builder.WithQueryParamIfNotNull("date", parameter.Date.ToString("yyyy-MM-dd"));
        }

        var response = await builder.GetAsync();
        return await HandleResponseAsync<CurrencyConversionResponseModel>(response);
    }

    /// <summary>
    /// Retrieves currency fluctuation data using the provided parameters.
    /// </summary>
    /// <param name="parameter">The fluctuation request parameters.</param>
    /// <returns>A <see cref="FluctuationResponseModel"/> containing the fluctuation data.</returns>
    /// <example>
    /// "https://api.apilayer.com/exchangerates_data/fluctuation?start_date={start_date}&amp;end_date={end_date}"
    /// </example>
    public async Task<FluctuationResponseModel> GetFluctuationAsync(FluctuationRequestParameters parameter)
    {
        var route = $"{FixUrlSection}/fluctuation";
        var builder = new HttpRequestBuilder(httpClient, route)
            .WithHeader("apikey", ApiKey)
            .WithQueryParam("start_date", parameter.StartDate.ToString("yyyy-MM-dd"))
            .WithQueryParam("end_date", parameter.EndDate.ToString("yyyy-MM-dd"));
        if (parameter.Base is not null)
        {
            builder.WithQueryParam("base", parameter.Base);
        }
        if (parameter.Symbols is not null)
        {
            builder.WithQueryParam("symbols", parameter.Symbols);
        }
        var response = await builder.SendAsync(HttpMethod.Get);
        return await HandleResponseAsync<FluctuationResponseModel>(response);
    }

    /// <summary>
    /// Retrieves the latest exchange rate data.
    /// </summary>
    /// <param name="parameter">The request parameters, including the base currency and the list of symbols.</param>
    /// <returns>The exchange rate response model.</returns>
    /// <example>
    /// "https://api.apilayer.com/exchangerates_data/latest?symbols={symbols}&amp;base={base}"
    /// </example>
    public async Task<ExchangeRateResponseModel> GetLatestRateAsync(ExchangeRateRequestParameters parameter)
    {
        var response = await httpClient
            .UsingRoute($"{FixUrlSection}/latest")
            .WithQueryParamIfNotNull("base", parameter.Base)
            .WithQueryParamIfNotNull("symbols", parameter.Symbols)
            .GetAsync();
        return await HandleResponseAsync<ExchangeRateResponseModel>(response);
    }

    /// <summary>
    /// Retrieves the list of currency symbols.
    /// </summary>
    /// <returns>The response containing the list of currency symbols.</returns>
    /// <example>
    /// "https://api.apilayer.com/exchangerates_data/symbols"
    /// </example>
    public async Task<CurrencySymbolsResponse> GetSymbolListAsync()
    {
        var response = await httpClient
            .UsingRoute($"{FixUrlSection}/symbols")
            .GetAsync();
        return await HandleResponseAsync<CurrencySymbolsResponse>(response);
    }

    /// <summary>
    /// Retrieves the historical exchange rate data for a specified timeframe.
    /// </summary>
    /// <param name="parameter">The request parameters, including the start date, end date, base currency, and list of symbols.</param>
    /// <returns>The historical exchange rate response.</returns>
    /// <example>
    /// "https://api.apilayer.com/exchangerates_data/timeseries?start_date={start_date}&amp;end_date={end_date}"
    /// </example>
    public async Task<GetTimeSeriesResponseModel> GetTimeSeriesAsync(GetTimeSeriesRequestParameters parameter)
    {
        var builder = httpClient
            .UsingRoute($"{FixUrlSection}/timeseries")
            .WithQueryParamIfNotNull("base", parameter.Base)
            .WithQueryParamIfNotNull("symbols", parameter.Symbols)
            .WithQueryParam("start_date", parameter.StartDate.ToString("yyyy-MM-dd"))
            .WithQueryParam("end_date", parameter.EndDate.ToString("yyyy-MM-dd"));

        var response = await builder.GetAsync();
        return await HandleResponseAsync<GetTimeSeriesResponseModel>(response);
    }

    /// <summary>
    /// Retrieves the historical exchange rate data for a specific date.
    /// </summary>
    /// <param name="parameter">The request parameters, including the date, base currency, and list of symbols.</param>
    /// <returns>The historical exchange rate response model.</returns>
    /// <example>
    /// "https://api.apilayer.com/exchangerates_data/{date}?symbols={symbols}&amp;base={base}"
    /// </example>
    public async Task<HistoricalRateResponseModel> GetHistoricalRatesAsync(HistoricalRateRequestParameters parameter)
    {
        var builder = await httpClient
            .UsingRoute($"{FixUrlSection}/{parameter.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}")
            .WithQueryParamIfNotNull("base", parameter.Base)
            .WithQueryParamIfNotNull("symbols", parameter.Symbols)
            .GetAsync();

        return await HandleResponseAsync<HistoricalRateResponseModel>(builder);
    }
}