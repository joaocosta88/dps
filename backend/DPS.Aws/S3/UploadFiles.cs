﻿using Amazon.S3.Transfer;

namespace DPS.Aws.S3;

public class UploadFileToS3Request
{
    public required string Filename { get; init; }
    public required Stream FileStream { get; init; }
}

public class UploadFileToS3Response
{
    public required string BucketName { get; init; }
    public required string Filename { get; init; }
}

public partial class S3Service
{
    public IList<AwsResponse<UploadFileToS3Response>> UploadFiles(IList<UploadFileToS3Request> req)
    {
        IList<Task<AwsResponse<UploadFileToS3Response>>> tasks = [];
        foreach (var item in req)
        {
            var t = UploadFileAsync(item);
            tasks.Add(t);
        }

        Task.WaitAll(tasks.ToArray());

        return tasks.Select(m => m.Result).ToList();
    }

    public async Task<AwsResponse<UploadFileToS3Response>> UploadFileAsync(UploadFileToS3Request req)
    {
        var fileTransferUtility = new TransferUtility(_s3Client);

        var transferUtilityUploadRequest = new TransferUtilityUploadRequest()
        {
            InputStream = req.FileStream,
            BucketName = _bucketName,
            Key = req.Filename,
            AutoCloseStream = true,
        };

        await fileTransferUtility.UploadAsync(transferUtilityUploadRequest);
        return new AwsResponse<UploadFileToS3Response>().SetSuccessResponse(new UploadFileToS3Response
        {
            BucketName = _bucketName,
            Filename = req.Filename,
        });
    }
}