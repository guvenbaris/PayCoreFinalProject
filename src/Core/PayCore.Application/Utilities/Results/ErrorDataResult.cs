namespace PayCore.Application.Utilities.Results;

public class ErrorDataResult : DataResult
{
    public virtual bool IsSuccess { get; set; } = false;
}
public class ErrorDataResult<T> : ErrorDataResult where T : class, new()
{
      public T? Data { get; set; }
}
