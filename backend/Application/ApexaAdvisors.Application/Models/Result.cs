using ApexaAdvisors.Domain.Resources;

namespace ApexaAdvisors.Application.Models;

public class Result<T>
{
    private readonly List<Error> _errors = new();

    public T? Data { get; protected set; } = default!;
    public bool IsError => _errors.Any();
    public IReadOnlyCollection<Error> Errors => _errors;

    private Result(T data) => Data = data;
    private Result(IEnumerable<string> codes) => AddErrorCore(codes);
    private Result(IEnumerable<Error> errors) => _errors = errors.ToList();

    public Result<T> AddError(params string[] codes)
    {
        AddErrorCore(codes);
        return this;
    }

    public Result<T> AddError(IEnumerable<string> codes)
    {
        AddErrorCore(codes);
        return this;
    }

    public static Result<T> Success(T data) => new(data);
    public static Result<T> Error(params string[] codes) => new(codes);
    public static Result<T> Error(IEnumerable<string> codes) => new(codes);
    public static Result<T> Error(Exception exception) =>
        new(new[]
        {
            new Error
            {
                Code = "UnknownError",
                Message = exception.Message
            }
        });

    private void AddErrorCore(IEnumerable<string> codes)
    {
        _errors.AddRange(codes.Select(e =>
            new Error
            {
                Code = e,
                Message = CommonResource.ResourceManager.GetString(e) ?? e
            }));
    }

    private void ClearErrorCore() => _errors.Clear();
}
