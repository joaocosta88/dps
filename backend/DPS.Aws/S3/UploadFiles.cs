using Amazon.S3.Transfer;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DPS.Aws.S3;

public class UploadFileToS3Request
{
	public string Filename { get; set; }
	public Stream FileStream { get; set; }
}

public class UploadFileToS3Response
{
	public string BucketName { get; set; }
	public string Filename { get; set; }
	public string Region { get; set; }
}

public partial class S3Service {

	public async Task<IList<AwsResponse<UploadFileToS3Response>>> UploadFiles(IList<UploadFileToS3Request> req)
	{
		IList<Task<UploadFileToS3Response>> tasks = [];
		foreach(var item in req)
		{
			var t = UploadFile(item);
			tasks.Add(t);
		}

		Task.WaitAll(tasks.ToArray());

		return tasks.Select(m => m.Result).ToList();
	}

	public async Task<AwsResponse<UploadFileToS3Response>> UploadFile(UploadFileToS3Request req)
	{
		using (IAmazonS3 client = new AmazonS3Client())
		{
			var fileTransferUtility = new TransferUtility(client);

			var transferUtilityUploadRequest = new TransferUtilityUploadRequest()
			{
				InputStream = req.FileStream,
				BucketName = _bucketName,
				Key = req.Filename,
				CannedACL = S3CannedACL.Private
			};

			bool success = true;
			try
			{
				await fileTransferUtility.UploadAsync(transferUtilityUploadRequest);
			}
			catch(Exception e)
			{
				success = false;
			}
			return new UploadFileToS3Response
			{
				BucketName = _bucketName,
				Filename = req.Filename,
				Region = _region.DisplayName
			};
		}
	}
}
