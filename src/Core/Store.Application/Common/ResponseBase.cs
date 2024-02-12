using System.Net;

namespace Store.Application.Common;

public record ResponseBase
{
    public ResponseBase()
    {
    }

    public ResponseBase(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public ResponseBase(string errorMessage, HttpStatusCode statusCode)
    {
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
    public string? ErrorMessage { get; init; }

    public static ResponseBase Failure(string errorMessage)
    {
        return new ResponseBase(errorMessage, HttpStatusCode.BadRequest);
    }

    public static ResponseBase Failure(string errorMessage, HttpStatusCode statusCode)
    {
        return new ResponseBase(errorMessage, statusCode);
    }

    public static ResponseBase Success(HttpStatusCode statusCode)
    {
        return new ResponseBase(statusCode);
    }

    public static ResponseBase Success()
    {
        return new ResponseBase(HttpStatusCode.OK);
    }
}