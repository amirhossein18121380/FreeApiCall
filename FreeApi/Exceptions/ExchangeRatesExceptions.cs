namespace FreeApi.Exceptions;

public class RateLimitMessage
{
    public string Message { get; set; }
}

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
}


public class JsonException : Exception
{
    public JsonException(string message) : base(message)
    {
    }
}

public class UnauthorizedAccessException : Exception
{
    public UnauthorizedAccessException(string message) : base(message)
    {
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}

public class ServerErrorException : Exception
{
    public ServerErrorException(string message) : base(message)
    {
    }
}

public class RateLimitExceededException : Exception
{
    public RateLimitExceededException(string message) : base(message)
    {
    }
}