using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace DPS.Aws.S3;

public partial class S3Service(string bucketName)
{
    private readonly string _bucketName = bucketName;
    private readonly RegionEndpoint _region = RegionEndpoint.EUCentral1;

    public string GetObjectUrl(string filename)
    {
        return $"https://{_bucketName}.s3.amazonaws.com/{filename}";
    }
    
}
