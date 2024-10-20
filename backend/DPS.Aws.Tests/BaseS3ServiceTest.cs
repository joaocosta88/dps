using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using DPS.Aws.S3;
using Microsoft.Extensions.Configuration;

namespace DPS.Aws.Tests;
public partial class S3ServiceTest {
	
	private const string BucketName = "dps--test--bucket";
	private static S3Service _s3Service = null!;

	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
	{
		var builder = new ConfigurationBuilder()
			.AddUserSecrets<S3ServiceTest>();
		var configuration = builder.Build();

		var accessKey = configuration["Aws:AccessKey"];
		var accessSecret = configuration["Aws:AccessSecret"];

		var s3Client = new AmazonS3Client(new BasicAWSCredentials(accessKey, accessSecret), RegionEndpoint.EUWest1);

		_s3Service = new S3Service(s3Client, BucketName);
	}

}
