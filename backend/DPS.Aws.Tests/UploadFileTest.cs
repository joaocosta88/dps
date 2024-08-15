using DPS.Aws.S3;
using DPS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Aws.Tests;

public partial class S3ServiceTest {
	[TestMethod]
	public async Task UploadFile()
	{
		//arrange
		var filename = $"testFile{Guid.NewGuid()}.txt";

		//act
		var res = await s3Service.UploadFileAsync(new S3.UploadFileToS3Request
		{
			Filename = filename,
			FileStream = Utils.GenerateStreamFromString("test file content")
		});

		//assert
		Assert.IsTrue(res.IsSucceed);

		//cleanup
		await s3Service.DeleteFileAsync(new DeleteS3FileRequest
		{
			Filename = filename
		});
	}
}