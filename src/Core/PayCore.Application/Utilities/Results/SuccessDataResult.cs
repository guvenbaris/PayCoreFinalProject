
namespace PayCore.Application.Utilities.Results;

public class SuccessDataResult : DataResult
{
    public new bool IsSuccess { get; set; } = true;
}
public class SuccessDataResult<T> : SuccessDataResult where T : class, new()
{   
    public new T? Data { get; set; }
}
