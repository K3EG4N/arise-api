using System.Text.Json;
using arise_api.dtos.Generics;
using Azure.Storage.Blobs;

namespace arise_api.services
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(FileUpload file, Guid EmployeeId);
    }

    public class BlobStorageService(BlobServiceClient blobServiceClient, IConfiguration configuration) : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient = blobServiceClient;
        private readonly string _containerName = configuration["AzureStorage:ContainerName"]!;

        public async Task<string> UploadAsync(FileUpload file, Guid EmployeeId)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync();

            var fileName = $"{EmployeeId}.{file.Extension}";
            var blobClient = containerClient.GetBlobClient(fileName);

            using var stream = new MemoryStream(file.FileData!);
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobClient.Uri.OriginalString;
        }
    }
}
