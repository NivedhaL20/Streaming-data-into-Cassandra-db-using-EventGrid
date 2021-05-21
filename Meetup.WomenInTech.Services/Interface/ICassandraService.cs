using Meetup.WomenInTech.CassandraRepository.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.WomenInTech.Services.Interface
{
    public interface ICassandraService
    {
        Task SubscribeEvent(UserDetails userDetails);

        Task<List<UserDetails>> Get();

        Task<UserDetails> GetById(string id);
    }
}
