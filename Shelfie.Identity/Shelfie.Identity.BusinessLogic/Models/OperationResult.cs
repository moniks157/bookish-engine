using Shelfie.Identity.BusinessLogic.Enums;

namespace Shelfie.Identity.BusinessLogic.Models;

public class OperationResult<T>
{
    public bool Success { get; set; } = false;

    public T? Result { get; set; }

    public ErrorCode ErrorCode { get; set; }
    public List<string> ValidationErrors { get; internal set; }
}
