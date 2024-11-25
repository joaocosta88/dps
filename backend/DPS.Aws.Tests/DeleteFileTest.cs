using DPS.Aws.S3;
using DPS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Aws.Tests;

public partial class S3ServiceTest
{
    [TestMethod]
    public async Task DeleteFile()
    {
        //arrange
        var filename = $"testFile{Guid.NewGuid()}.txt";
        await _s3Service.UploadFileAsync(new UploadFileToS3Request
        {
            Filename = filename,
            FileStream = Utils.GenerateStreamFromString("test file content")
        });

        //act
        var res = await _s3Service.DeleteFileAsync(new DeleteS3FileRequest
        {
            Filename = filename
        });

        //asert
        Assert.IsTrue(res.Success);
    }
}