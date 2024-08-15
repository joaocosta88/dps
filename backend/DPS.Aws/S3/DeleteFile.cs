using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Aws.S3;

public class DeleteS3FileRequest
{
	public string Filename { get; set; }
}


public partial class S3Service {

	public async Task<AwsResponse<bool>> DeleteFileAsync(DeleteS3FileRequest request)
	{
		try
		{
			var deleteObjectRequest = new DeleteObjectRequest
			{
				BucketName = _bucketName,
				Key = request.Filename,
			};

			var response = await _s3Client.DeleteObjectAsync(deleteObjectRequest);

			return new AwsResponse<bool>().SetSuccessResponse(true);
		}
		catch(Exception e)
		{
			return new AwsResponse<bool>().SetSuccessResponse(false);
		}
	}
} 
