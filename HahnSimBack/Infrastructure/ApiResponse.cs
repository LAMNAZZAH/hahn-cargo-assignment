using System.Net;

namespace HahnCargoAutomation.Server.Infrastructure
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(bool success, string message, T data, HttpStatusCode statusCode)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = (int)statusCode;
            Errors = new List<string>();
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>(true, message, data, HttpStatusCode.OK);
        }

        public static ApiResponse<T> ErrorResponse(string message, HttpStatusCode statusCode, List<string> errors = null)
        {
            var response = new ApiResponse<T>(false, message, default, statusCode);
            if (errors != null)
            {
                response.Errors.AddRange(errors);
            }
            return response;
        }
    }
}

