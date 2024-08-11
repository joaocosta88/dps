﻿namespace DPS.Service {
	public class AppResponse<T> {
		public bool IsSucceed { get; set; } = true;
		public Dictionary<string, string[]> Messages { get; set; } = [];
		public T? Data { get; set; }

		internal AppResponse<T> SetSuccessResponse(T data)
		{
			Data = data;
			return this;
		}
		internal AppResponse<T> SetSuccessResponse(T data, string key, string value)
		{
			Data = data;
			Messages.Add(key, [value]);
			return this;
		}
		internal AppResponse<T> SetSuccessResponse(T data, Dictionary<string, string[]> message)
		{
			Data = data;
			Messages = message;
			return this;
		}
		internal AppResponse<T> SetSuccessResponse(T data, string key, string[] value)
		{
			Data = data;
			Messages.Add(key, value);
			return this;
		}
		internal AppResponse<T> SetErrorResponse(string key, string value)
		{
			IsSucceed = false;
			Messages.Add(key, [value]);
			return this;
		}
		internal AppResponse<T> SetErrorResponse(string key, string[] value)
		{
			IsSucceed = false;
			Messages.Add(key, value);
			return this;
		}
		internal AppResponse<T> SetErrorResponse(Dictionary<string, string[]> message)
		{
			IsSucceed = false;
			Messages = message;
			return this;
		}
	}
}
