
using PayCore.Application.Utilities.Results;

namespace PayCore.Application.Utilities.BusinessRuleEngine
{
    public class BusinessRuleEngine
    {
        public static DataResult Validate(params IDataResult[] rules)
        {
            foreach (var result in rules)
            {
                if (!result.IsSuccess)
                {
                    return new ErrorDataResult{ ErrorMessage = result.ErrorMessage, Data = result.Data};
                }
            }
            return new SuccessDataResult();
        }
    }
}
