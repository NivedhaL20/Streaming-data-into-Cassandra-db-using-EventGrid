using Meetup.WomenInTech.CassandraRepository.Interface;
using Meetup.WomenInTech.CassandraRepository.Model;
using Meetup.WomenInTech.Services.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.WomenInTech.Services.Implementation
{
    public class CassandraService: ICassandraService
    {

        private readonly IRepository _repository;

        public CassandraService(IRepository repository)
        {            
            _repository = repository;
        }

        public async Task SubscribeEvent(UserDetails userDetails)
        {
            await _repository.Upsert(userDetails);
        }

        public async Task<List<UserDetails>> Get()
        {
            return await _repository.Get();
        }

        public async Task<UserDetails> GetById(string id)
        {
            return await _repository.GetById(id);
        }
    }
}
