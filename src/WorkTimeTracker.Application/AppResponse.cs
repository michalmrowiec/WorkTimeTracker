using FluentValidation.Results;

namespace WorkTimeTracker.Application
{
    public class AppResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? ValidationErrors { get; set; }

        public AppResponse()
        {
            Success = true;
            ValidationErrors = new();
        }

        public AppResponse(bool status, string message)
        {
            Success = status;
            Message = message;
            ValidationErrors = new();
        }

        public AppResponse(ValidationResult validationResult)
        {
            Success = false;
            ValidationErrors = new();
            validationResult.Errors
                .ForEach(e => ValidationErrors.Add(e.ErrorMessage));
        }

        public AppResponse(bool success, string? message, ValidationResult validationResult)
        {
            Success = success;
            Message = message;
            ValidationErrors = new();
            validationResult.Errors
                .ForEach(e => ValidationErrors.Add(e.ErrorMessage));
        }
    }

    public class AppResponse<T> : AppResponse where T : class
    {
        public T? ReturnedObj { get; set; }

        public AppResponse(T obj)
        {
            Success = true;
            ValidationErrors = [];
            ReturnedObj = obj;
        }

        public AppResponse(ValidationResult validationResult) : base(validationResult) { }

        public AppResponse(bool status, string message) : base(status, message) { }
    }
}