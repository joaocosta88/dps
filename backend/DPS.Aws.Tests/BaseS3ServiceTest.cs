using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using DPS.Aws.S3;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Aws.Tests;
public partial class S3ServiceTest {

	private static S3Service s3Service;
	private static string _bucketName = "dps--test--bucket";

	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
	{
		var builder = new ConfigurationBuilder()
			.AddUserSecrets<S3ServiceTest>();
		var configuration = builder.Build();

		var accessKey = configuration["Aws:AccessKey"];
		var accessSecret = configuration["Aws:AccessSecret"];

		var s3Client = new AmazonS3Client(new BasicAWSCredentials(accessKey, accessSecret), RegionEndpoint.EUWest1);

		s3Service = new S3Service(s3Client, _bucketName);
	}

}
