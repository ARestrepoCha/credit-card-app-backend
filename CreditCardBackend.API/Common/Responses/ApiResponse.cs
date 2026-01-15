using System.Net;

namespace CreditCardBackend.API.Common.Responses
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public T? Data { get; set; }

        public string? Message { get; set; }

        // Success response
        public static ApiResponse<T> Success(HttpStatusCode statusCode, T data, string message = "Operación exitosa")
        {
            return new ApiResponse<T> { Status = true, StatusCode = statusCode, Data = data, Message = message };
        }

        // Error response
        public static ApiResponse<T> Error(HttpStatusCode statusCode, string message, object? errorData = null)
        {
            return new ApiResponse<T> { Status = false, StatusCode = statusCode, Message = message, Data = (T?)errorData };
        }
    }

    public class ApiResponse
    {
        public bool Status { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public object? Data { get; set; }

        public string? Message { get; set; }

        // Success response
        public static ApiResponse Success(HttpStatusCode statusCode, string message = "Operación exitosa", object? data = null)
        {
            return new ApiResponse { Status = true, StatusCode = statusCode, Message = message, Data = data };
        }

        // Error response
        public static ApiResponse Error(HttpStatusCode statusCode, string message)
        {
            return new ApiResponse { Status = false, StatusCode = statusCode, Message = message };
        }
    }
}
