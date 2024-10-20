using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Aws;
public class AwsResponse<T> {
	public bool Success { get; set; } = true;
	public T? Data { get; set; }
	public Dictionary<string, string[]> Messages { get; set; } = [];

	internal AwsResponse<T> SetSuccessResponse(T data)
	{
		Data = data;
		return this;
	}
	internal AwsResponse<T> SetSuccessResponse(T data, string key, string value)
	{
		Data = data;
		Messages.Add(key, [value]);
		return this;
	}
	internal AwsResponse<T> SetSuccessResponse(T data, Dictionary<string, string[]> message)
	{
		Data = data;
		Messages = message;
		return this;
	}
	internal AwsResponse<T> SetSuccessResponse(T data, string key, string[] value)
	{
		Data = data;
		Messages.Add(key, value);
		return this;
	}
	internal AwsResponse<T> SetErrorResponse(string key, string value)
	{
		Success = false;
		Messages.Add(key, [value]);
		return this;
	}
	internal AwsResponse<T> SetErrorResponse(string key, string[] value)
	{
		Success = false;
		Messages.Add(key, value);
		return this;
	}
	internal AwsResponse<T> SetErrorResponse(Dictionary<string, string[]> message)
	{
		Success = false;
		Messages = message;
		return this;
	}
}
