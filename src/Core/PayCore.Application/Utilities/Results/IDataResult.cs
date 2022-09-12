using PayCore.Application.Models;

namespace PayCore.Application.Utilities.Results;

public interface IDataResult
{
    BaseModel? Data { get; set; }
    string? ErrorMessage { get; set; }
    bool IsSuccess { get; set; }
}