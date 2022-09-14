
namespace PayCore.Application.Utilities.Results;

public class SuccessDataResult : DataResult
{
    public override bool IsSuccess { get; set; } = true;
}
public class SuccessDataResult<T> : SuccessDataResult where T : class, new()
{   
    public T? Data { get; set; }
}
