using Shelfie.Identity.BusinessLogic.Enums;

namespace Shelfie.Identity.BusinessLogic.Models;

public class OperationResult<T>
{
    public bool IsSuccess { get; set; } = false;

    public T? Data { get; set; }

    public ErrorCode ErrorCode { get; set; }

    public List<string>? ValidationErrors { get; internal set; }

    public static OperationResult<T> Success(T? result)
    {
        return new()
        {
            IsSuccess = true,
            Data = result,
        };
    }

    public static OperationResult<T> ValidationFailure(List<string> validationErrors)
    {
        return new()
        {
            IsSuccess = false,
            ErrorCode = ErrorCode.InvalidRequest,
            ValidationErrors = validationErrors,
        };
    }

    public static OperationResult<T> Failure(ErrorCode errorCode)
    {
        return new()
        {
            IsSuccess = false,
            ErrorCode = errorCode,
        };
    }
}
