using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Aws.S3;

public class DeleteS3FileRequest
{
	public string BucketName { get; set }
	public string Filename { get; set; }
}


public partial class S3Service {

	public async Task<AwsResponse<bool>> DeleteFile(DeleteS3FileRequest request)
	{
		var deleteObjectRequest = new DeleteObjectRequest
		{
			BucketName = request.BucketName,
			Key = request.Filename,
		};

		var response = await _s3Client.DeleteObjectAsync(deleteObjectRequest);
		return new AwsResponse<bool>().SetSuccessResponse(true);
	}
} 
