using FreeApi.Exceptions;
using FreeApi.RequestHelper;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnauthorizedAccessException = FreeApi.Exceptions.UnauthorizedAccessException;

namespace FreeApi.Services;

public abstract class ApiClientBase
{
    protected async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                PropertyNameCaseInsensitive = true
            };

            return await response.GetResponseStreamAsync().DeserializeJsonAsync<T>(options);
        }

        // Check for rate limit exceeded
        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            var rateLimitMessage = await response.DeserializeJsonAsync<RateLimitMessage>();
            throw new RateLimitExceededException(rateLimitMessage.Message);
        }

        // Check for other error codes
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new BadRequestException("The request was unacceptable, often due to missing a required parameter || Invalid Input");
        }
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("No valid API key provided.");
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new NotFoundException("The requested resource doesn't exist.");
        }
        else if (response.StatusCode >= HttpStatusCode.InternalServerError)
        {
            throw new ServerErrorException("We have failed to process your request. (You can contact us anytime)");
        }

        // If none of the above conditions match, throw a general exception
        throw new Exception("An unexpected error occurred.");
    }
}