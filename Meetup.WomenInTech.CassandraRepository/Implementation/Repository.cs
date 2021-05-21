using Cassandra.NetCore.ORM;
using Meetup.WomenInTech.CassandraRepository.Interface;
using Meetup.WomenInTech.CassandraRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.WomenInTech.CassandraRepository.Implementation
{
    public class Repository : IRepository
    {
        private readonly ICassandraDbContext _dbContext;

        public Repository(ICassandraDbContext cassandraDb)
        {
            _dbContext = cassandraDb;
        }

        public async Task<List<UserDetails>> Get()
        {
            var result = (await _dbContext.SelectAsync<UserDetails>()).ToList();
            
            return result;
        }

        public async Task Upsert(UserDetails userDetails)
        {
            await _dbContext.AddOrUpdateAsync(userDetails);
        }

        public async Task<UserDetails> GetById(string id)
        {
            var result = await _dbContext.FirstOrDefaultAsync<UserDetails>(m => m.Id == id);

            return result;
        }
    }
}
