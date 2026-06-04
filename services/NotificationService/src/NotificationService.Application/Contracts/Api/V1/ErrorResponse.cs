namespace NotificationService.Application.Contracts.Api.V1;

public sealed record ErrorResponse(
    ErrorBody Error)
{
    public static ErrorResponse Validation(string field, string issue, string message)
    {
        return new ErrorResponse(new ErrorBody(
            "validation_error",
            message,
            new[] { new ErrorDetail(field, issue) },
            null));
    }

    public static ErrorResponse NotFound(string code, string message)
    {
        return new ErrorResponse(new ErrorBody(code, message, Array.Empty<ErrorDetail>(), null));
    }
}

public sealed record ErrorBody(
    string Code,
    string Message,
    IReadOnlyCollection<ErrorDetail> Details,
    string? CorrelationId);

public sealed record ErrorDetail(
    string Field,
    string Issue);