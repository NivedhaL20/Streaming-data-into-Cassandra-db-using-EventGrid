// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Meetup.WomenInTech.Services.Interface;
using Meetup.WomenInTech.Services.Model;
using System.Threading.Tasks;

namespace Meetup.WomenInTech.PushEventToBlobFuncApp
{
    public class PushToBlob
    {
        private readonly IBlobStorageService _blobStorageService;

        public PushToBlob(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [FunctionName("PushToBlob")]
        public async Task Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation("Event Grid trigger function started");

            var data = JsonConvert.DeserializeObject<User>(eventGridEvent.Data.ToString());

            var blobName = $"{data.Id}";

            await using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(eventGridEvent.Data.ToString())))
            {
                await _blobStorageService.UploadStreamIntoBlobAsync(blobName, ms);
            }
            log.LogInformation("Event Grid trigger function executed successfully");
        }
    }
}
