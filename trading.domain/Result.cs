namespace trading.domain;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Success<T>(T value) => new(true, value, default);
    public static Result<T> Failure<T>(Error error) => new(false, default, error);

}

public class Result<T> : Result
{
    public T? Value { get; }

    protected internal Result(bool isSuccess, T? value, Error error) : base(isSuccess, error)
    {
        Value = value;
    }
}

public record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}