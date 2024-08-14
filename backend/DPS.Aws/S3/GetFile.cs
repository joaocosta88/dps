using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Aws.S3;

public class GetS3FileRequest {
	public string BucketName { get; set }
	public string Filename { get; set; }
}

public partial class S3Service {

	public async Task<AwsResponse<Stream>> GetFile(GetS3FileRequest request)
	{
		var getObjectRequest = new GetObjectRequest
		{
			BucketName = request.BucketName,
			Key = request.Filename,
		};

		using GetObjectResponse response = await _s3Client.GetObjectAsync(getObjectRequest);

		return new AwsResponse<Stream>().SetSuccessResponse(response.ResponseStream);
	}
}
