using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Meetup.WomenInTech.Services.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.WomenInTech.Services.Implementation
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly string _blobConnection;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            _blobConnection = configuration.GetConnectionString("BlobConnection") ?? Environment.GetEnvironmentVariable("BlobConnection");
            _containerName = configuration.GetConnectionString("ContainerName") ?? Environment.GetEnvironmentVariable("ContainerName");
        }

        public async Task UploadStreamIntoBlobAsync(string blobName, Stream stream)
        {
            try
            {
                var container = new BlobContainerClient(_blobConnection, _containerName);

                // Create container if not exists
                var blockBlob = await container.CreateIfNotExistsAsync();

                //Retrieve reference to a blob named
                var blobClient = container.GetBlobClient(blobName);

                var blobHttpHeader = new BlobHttpHeaders { ContentType = "application/json" };

                await blobClient.UploadAsync(stream, blobHttpHeader).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
