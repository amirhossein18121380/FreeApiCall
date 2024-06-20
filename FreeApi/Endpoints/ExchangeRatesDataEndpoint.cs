using FreeApi.Parameters.ExchangeRates;
using FreeApi.Responses.ExchangeRates;
using FreeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace FreeApi.Endpoints;
public static class ExchangeRatesDataEndpoint
{
    public static void MapExchangeRatesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/convert",
                async ([FromServices] ICurrencyConversionService myService,
                    [FromBody] CurrencyConversionParameters parameters) =>
                {
                    //example:
                    //parameters.From = "GBP";
                    //parameters.To = "JPY";
                    //parameters.Amount = 25;
                    //parameters.WithDate = false;
                    var data = await myService.ConvertCurrencyAsync(parameters);
                    return Results.Ok(data);
                })
            .WithMetadata(new SwaggerResponseExampleAttribute(200, typeof(CurrencyConversionParametersExample)))
            .WithName("Convert")
            .Produces<CurrencyConversionResponseModel>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Convert currency")
            .WithDescription("Convert any amount from one currency to another.");

        app.MapPost("/fluctuation", async ([FromServices] ICurrencyConversionService myService,
            [FromBody] FluctuationRequestParameters parameters) =>
        {
            //example:
            //parameters.StartDate = DateTime.ParseExact("2020-04-02", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //parameters.EndDate = DateTime.ParseExact("2024-04-02", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //parameters.Base = "EUR";
            //parameters.Symbols = String.Empty;
            var data = await myService.GetFluctuationAsync(parameters);
            return Results.Ok(data);
        })
        .WithName("GetFluctuation")
        .Produces<FluctuationResponseModel>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get currency fluctuation")
        .WithDescription("This endpoint returns the fluctuation data between specified dates." +
                         " The data can be for all available currencies or for a specific set.");

        app.MapPost("/latest",
            async ([FromServices] ICurrencyConversionService myService, [FromBody] ExchangeRateRequestParameters parameters) =>
            {
                //example:
                //parameters.Base = "EUR";
                //parameters.Symbols = string.Empty;

                var data = await myService.GetLatestRateAsync(parameters);
                return Results.Ok(data); ;
            })
            .WithName("GetLatestRate")
            .Produces<ExchangeRateResponseModel>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get latest exchange rate")
            .WithDescription("Returns exchange rate data in real-time for all available currencies or for a specific set.");

        app.MapPost("/symbols",
                async ([FromServices] ICurrencyConversionService myService) =>
                {
                    var data = await myService.GetSymbolListAsync();
                    return Results.Ok(data);
                })
            .WithName("GetSymbols")
            .Produces<CurrencySymbolsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get currency symbols")
            .WithDescription("Returns all available currencies.");

        app.MapPost("/timeSeries", async ([FromServices] ICurrencyConversionService myService,
                [FromBody] GetTimeSeriesRequestParameters parameters) =>
            {
                //example:
                //parameters.StartDate = DateTime.ParseExact("2020-04-03", "yyyy-MM-dd", CultureInfo.InvariantCulture);
                //parameters.EndDate = DateTime.ParseExact("2020-04-04", "yyyy-MM-dd", CultureInfo.InvariantCulture);
                //parameters.Base = string.Empty;
                //parameters.Symbols = string.Empty;

                if (!parameters.IsValid())
                {
                    return Results.Ok();
                }

                var data = await myService.GetTimeSeriesAsync(parameters);
                return Results.Ok(data);
            }).WithName("GetTimeSeries")
            .Produces<GetTimeSeriesResponseModel>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get time series of exchange rates")
            .WithDescription("It returns the daily historical data for exchange rates, between two specified dates. " +
                             "The data can be returned for all available currency or for specified ones.");

        app.MapPost("/history", async ([FromServices] ICurrencyConversionService myService,
               [FromBody] HistoricalRateRequestParameters parameters) =>
        {
            //example:
            //parameters.Date = DateTime.ParseExact("2020-04-02", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //parameters.Base = string.Empty;
            //parameters.Symbols = string.Empty;

            var data = await myService.GetHistoricalRatesAsync(parameters);
            return Results.Ok(data);
        })
        .WithName("GetHistoricalRates")
        .Produces<HistoricalRateResponseModel>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get historical exchange rates")
        .WithDescription("Endpoint for receiving historical exchange rate information for all available currencies or for a set of currencies.");
    }
}

public class CurrencyConversionParametersExample : IExamplesProvider<CurrencyConversionParameters>
{
    public CurrencyConversionParameters GetExamples()
    {
        return new CurrencyConversionParameters
        {
            From = "EUR",
            To = "GBP",
            Amount = 10
        };
    }
}
