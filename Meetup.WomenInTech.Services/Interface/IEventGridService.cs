using Meetup.WomenInTech.CassandraRepository.Model;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.WomenInTech.Services.Interface
{
    public interface IEventGridService
    {
        string BuildSharedAccessSignature();
        Task<HttpResponseMessage> PostEventUsingSas(List<EventGridEvent> input);        
    }
}
