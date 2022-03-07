using System;
namespace Project.WebFrameworks.Api
{
    public class CustomException : Exception
    {
        private readonly string _message;

        public CustomException(string message, int statusCode)
        {
            StatusCode = statusCode;
            _message = message;
        }

        public int StatusCode { get; set; }

        public override string Message
        {
            get
            {
                return _message;
            }
        }
        
    }
}
