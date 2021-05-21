using Meetup.WomenInTech.CassandraRepository.Interface;
using Meetup.WomenInTech.CassandraRepository.Model;
using Meetup.WomenInTech.Services.Interface;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Meetup.WomenInTech.Services.Implementation
{
    public class EventGridService : IEventGridService
    {
        private readonly string _topicEndpoint;
        private readonly string _topicKey;
        private readonly double _expiresIn;
        private readonly IRepository _repository;
       
        public EventGridService(IConfiguration configuration, IRepository repository)
        {
            _topicEndpoint = configuration["EventGrid:TopicEndpoint"];
            _topicKey = configuration["EventGrid:TopicKey"];
            _expiresIn = Convert.ToDouble(configuration["EventGrid:ExpiresInHours"]);
            _repository = repository;
        }

        public string BuildSharedAccessSignature()
        {
            var expirationUtc = DateTime.Now.AddHours(_expiresIn);
            const char letterR = 'r';
            const char letterE = 'e';
            const char letterS = 's';

            var encodedResource = HttpUtility.UrlEncode(_topicEndpoint);
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            var encodedExpirationUtc = HttpUtility.UrlEncode(expirationUtc.ToString(culture));

            var unsignedSas = $"{letterR}={encodedResource}&{letterE}={encodedExpirationUtc}";
            using var hmac = new HMACSHA256(Convert.FromBase64String(_topicKey));

            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(unsignedSas)));
            var encodedSignature = HttpUtility.UrlEncode(signature);
            var signedSas = $"{unsignedSas}&{letterS}={encodedSignature}";

            return signedSas;
        }

        public async Task<HttpResponseMessage> PostEventUsingSas(List<EventGridEvent> input)
        {
            var dd = GetEvents();
            var client = new HttpClient();

            var sasToken = BuildSharedAccessSignature();

            client.DefaultRequestHeaders.Add("aeg-sas-token", sasToken);

            var json = JsonConvert.SerializeObject(dd);

            var request = new HttpRequestMessage(HttpMethod.Post, _topicEndpoint)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            return response;
        }

        private static List<EventGridEvent> GetEvents()
        {
            var eventsList = new List<EventGridEvent>();
            for (var i = 0; i < 3; i++)
            {
                eventsList.Add(new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = (i % 2 == 0) ? "evenType" : "oddType",
                    Data = new UserDetails
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = $"firstname {i}",
                        LastName = $"lastname {i}"
                    },

                    EventTime = DateTime.Now,
                    Subject = $"Event {i}",
                    DataVersion = "2.0"
                });
            }
            return eventsList;
        }
    }
}
