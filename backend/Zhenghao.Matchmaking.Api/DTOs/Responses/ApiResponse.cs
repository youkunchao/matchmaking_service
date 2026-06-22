namespace Zhenghao.Matchmaking.Api.DTOs.Responses;

public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T? data = default)
    {
        return new ApiResponse<T> { Code = 0, Message = "成功", Data = data };
    }

    public static ApiResponse<T> Success(string message, T? data = default)
    {
        return new ApiResponse<T> { Code = 0, Message = message, Data = data };
    }

    public static ApiResponse<T> Error(int code, string message)
    {
        return new ApiResponse<T> { Code = code, Message = message };
    }

    public static ApiResponse<T> Error(string message)
    {
        return new ApiResponse<T> { Code = -1, Message = message };
    }
}

public class ApiResponse
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;

    public static ApiResponse Success()
    {
        return new ApiResponse { Code = 0, Message = "成功" };
    }

    public static ApiResponse Success(string message)
    {
        return new ApiResponse { Code = 0, Message = message };
    }

    public static ApiResponse Error(int code, string message)
    {
        return new ApiResponse { Code = code, Message = message };
    }

    public static ApiResponse Error(string message)
    {
        return new ApiResponse { Code = -1, Message = message };
    }
}