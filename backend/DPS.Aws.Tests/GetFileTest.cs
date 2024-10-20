using DPS.Aws.S3;
using DPS.Common;

namespace DPS.Aws.Tests;

[TestClass]
public partial class S3ServiceTest{
	[TestMethod]
	public async Task TestGetFile()
	{
		//arrange
		var filename = $"testFile{Guid.NewGuid()}.txt";
		await _s3Service.UploadFileAsync(new UploadFileToS3Request
		{
			Filename = filename,
			FileStream = Utils.GenerateStreamFromString("test file content")
		});

		//act
		var res = await _s3Service.GetFile(new GetS3FileRequest
		{
			Filename = filename
		});

		Assert.IsTrue(res.Success);
		Assert.IsTrue(res.Data!.Length > 0);

		//cleanup
		await _s3Service.DeleteFileAsync(new DeleteS3FileRequest
		{
			Filename = filename
		});
	}
}