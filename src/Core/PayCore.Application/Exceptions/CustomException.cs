using System;

namespace PayCore.Application.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
