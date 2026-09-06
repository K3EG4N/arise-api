using Arise.Application.Common.Enums;

namespace Arise.Application.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public string? Message { get; }
        public ErrorType ErrorType { get; }
        public Dictionary<string, string[]>? ValidationErrors { get; }

        protected Result(bool isSuccess, string? error, ErrorType errorType, Dictionary<string, string[]>? validationErrors = null, string? message = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            ErrorType = errorType;
            Message = message;
            ValidationErrors = validationErrors;
        }

        public static Result Success(string? message = null) => new(true, null, default, message: message ?? null);

        public static Result Failure(string error, ErrorType errorType = ErrorType.Failure) =>
            new(false, error, errorType);

        public static Result ValidationFailure(Dictionary<string, string[]> errors) =>
            new(false, "One or more validation errors occurred.", ErrorType.Validation, errors);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(bool isSuccess, T? value, string? error, ErrorType errorType, Dictionary<string, string[]>? validationErrors = null, string? message = null)
            : base(isSuccess, error, errorType, validationErrors, message)
        {
            Value = value;
        }

        public static Result<T> Success(T value, string? message = null) => new(true, value, null, default, message: message ?? null);

        public static new Result<T> Failure(string error, ErrorType errorType = ErrorType.Failure) =>
            new(false, default, error, errorType, null);

        public static new Result<T> ValidationFailure(Dictionary<string, string[]> errors) =>
            new(false, default, "One or more validation errors occurred.", ErrorType.Validation, errors, null);
    }
}

