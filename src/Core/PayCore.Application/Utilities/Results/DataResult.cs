using PayCore.Application.Models;

namespace PayCore.Application.Utilities.Results;

public class DataResult : IDataResult
{
    public  BaseModel? Data { get; set; }

    public virtual bool IsSuccess { get; set; }

    public string? ErrorMessage { get; set; }
    public string?  Message { get; set; }
}
public class DataResult<T> : DataResult where T : class, new()
{
    public T? Data { get; set; }
}
