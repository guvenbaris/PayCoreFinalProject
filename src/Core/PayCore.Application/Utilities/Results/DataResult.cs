using PayCore.Application.Models;

namespace PayCore.Application.Utilities.Results;

public class DataResult : IDataResult
{
    public BaseModel? Data { get; set; }

    public bool IsSuccess { get; set; } = false;

    public string? ErrorMessage { get; set; }
}
public class DataResult<T> : DataResult where T : class, new()
{
    public new T? Data { get; set; }
}
