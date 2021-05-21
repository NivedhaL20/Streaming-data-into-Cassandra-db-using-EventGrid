// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Meetup.WomenInTech.CassandraRepository.Model;
using Meetup.WomenInTech.Services.Interface;

namespace Meetup.WomenInTech.EventGridFuncApp
{
    public class PushEventToCassandraDb
    {
        private readonly ICassandraService _cassandraService;

        public PushEventToCassandraDb(ICassandraService cassandraService)
        {
            _cassandraService = cassandraService;
        }

        [FunctionName("PushToCassandraDb")]
        public async Task Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            try
            {
                log.LogInformation("Event Grid trigger function started");

                var userDetails = JsonConvert.DeserializeObject<UserDetails>(eventGridEvent.Data.ToString());

                await _cassandraService.SubscribeEvent(userDetails);

                log.LogInformation("Event Grid trigger function executed successfully");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
