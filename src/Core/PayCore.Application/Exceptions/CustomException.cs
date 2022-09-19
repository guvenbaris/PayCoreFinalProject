using System.Net;
using System.Runtime.Serialization;

namespace PayCore.Application.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.BadRequest;
        public List<string> ErrrorList { get; set; }

        public CustomException() : base()
        {

        }
        public CustomException(string message) : base(message) { }


        public CustomException(string message, List<string> Errors, HttpStatusCode httpStatusCode) : base(message)
        {
            this.ErrrorList = Errors;
            this.HttpStatusCode = httpStatusCode;
        }

        public CustomException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
        }
        public CustomException(string message, System.Exception inner) : base(message, inner)
        {

        }

        public CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
