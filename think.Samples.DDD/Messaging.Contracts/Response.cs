using System;

namespace Messaging.Contracts
{
    public class Response
    {
        public bool IsSuccessful { get; private set; }
        public Exception Exception { get; private set; }
        public string Message { get; private set; }

        public Response(bool isSuccessful, Exception exception = null, string message = null)
        {
            IsSuccessful = isSuccessful;
            Exception = exception;
            Message = message;
        }
        
        public static Response Unsuccessful(string message = null, Exception exception = null)
        {
            return new Response(false, exception, message);
        }

        public static Response Success()
        {
            return new Response(true);
        }
    }
}