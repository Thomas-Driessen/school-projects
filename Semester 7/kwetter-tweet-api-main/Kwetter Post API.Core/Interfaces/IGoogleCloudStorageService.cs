using Microsoft.AspNetCore.Http;

namespace Kwetter_Post_API.Core.Interfaces;

public interface IGoogleCloudStorageService
{
    Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);
    Task DeleteFileAsync(string fileNameForStorage);
}