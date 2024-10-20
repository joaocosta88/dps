using Amazon.S3.Model;

namespace DPS.Aws.S3;

public class GetS3FileRequest {
	public required string Filename { get; set; }
}

public partial class S3Service {

	public async Task<AwsResponse<Stream>> GetFile(GetS3FileRequest request)
	{
		var getObjectRequest = new GetObjectRequest
		{
			BucketName = _bucketName,
			Key = request.Filename,
		};

		using GetObjectResponse response = await _s3Client.GetObjectAsync(getObjectRequest);

		return new AwsResponse<Stream>().SetSuccessResponse(response.ResponseStream);
	}
}
