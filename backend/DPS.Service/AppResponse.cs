namespace DPS.Service {

	public class AppResponseError
	{
		public required string ErrorCode { get; init; }
		public string? Message { get; init; }
		public object? Details { get; init; }
	}
	
	public class AppResponse<T> {
		public bool Success { get; init; }
		public T? Data { get; init; }
		public AppResponseError? Error { get; init; }
		
		private AppResponse() {}

		public static AppResponse<T> GetSuccessResponse(T data)
		{
			return new AppResponse<T>()
			{
				Success = true,
				Data = data,
			};
		}

		public static AppResponse<T> GetErrorResponse(String errorCode, String? errorMessage = null, object? errorDetails = null)
		{
			return new AppResponse<T>()
			{
				Success = false,
				Error = new AppResponseError()
				{
					ErrorCode = errorCode,
					Message = errorMessage,
					Details = errorDetails
				}
			};
		}
	}
}
