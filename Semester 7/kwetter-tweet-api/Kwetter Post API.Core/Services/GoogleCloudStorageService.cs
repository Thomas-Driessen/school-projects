using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Kwetter_Post_API.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Kwetter_Post_API.Core.Services;

public class GoogleCloudStorageService : IGoogleCloudStorageService
{
    private readonly GoogleCredential googleCredential;
    private readonly StorageClient storageClient;
    private readonly string bucketName;

    public GoogleCloudStorageService(IConfiguration configuration)
    {
        googleCredential = GoogleCredential.FromFile(configuration["GoogleCloudStorage:GCPStorageAuthFile"]);
        storageClient = StorageClient.Create(googleCredential);
        bucketName = configuration["GoogleCloudStorage:GoogleCloudStorageBucketName"];
    }

    public async Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage)
    {
        using var memoryStream = new MemoryStream();
        await imageFile.CopyToAsync(memoryStream);
        var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, memoryStream);
        return dataObject.MediaLink;
    }

    public async Task DeleteFileAsync(string fileNameForStorage)
    {
        await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
    }
}