namespace MindHive.Application.DTOs.CommonModels;

public class BaseResponseModel<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static BaseResponseModel<T> Ok(T data, string? message = null)
        => new BaseResponseModel<T> { Success = true, Data = data, Message = message };

    public static BaseResponseModel<T> Fail(string message, string? errorCode = null, List<string>? errors = null)
        => new BaseResponseModel<T> { Success = false, Message = message, ErrorCode = errorCode, Errors = errors };
}