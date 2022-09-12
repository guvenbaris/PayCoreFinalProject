namespace PayCore.Application.Utilities.Results;

public class ErrorDataResult : DataResult
{
    public new bool IsSuccess { get; set; } = false;
}
public class ErrorDataResult<T> : ErrorDataResult where T : class, new()
{
      public new T? Data { get; set; }
}
