namespace Bobflix_Backend.ApiResponseType
{
    public class ApiResponseType<T>
    {
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ApiResponseType(bool success, string errorMessage, T data) { 
            Success = success;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public ApiResponseType(bool v) { }

 
    }
}
