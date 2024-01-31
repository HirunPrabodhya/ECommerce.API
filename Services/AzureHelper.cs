using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AzureHelper
    {
        public static async Task<String> SendToAsureDb(IFormFile file)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=myshopstorage;AccountKey=JtMBycrrC393wDWgNvutquZ5bEyjsmWpkk87KeIPnMZu+y4k+rwNCIdKQGwtV/9kcKZizdsk17i1+ASt+BTqfg==;EndpointSuffix=core.windows.net";
            string containerName = "categories";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);

            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream, overwrite: true);

            return blobClient.Uri.AbsoluteUri;
        }
    }
}

