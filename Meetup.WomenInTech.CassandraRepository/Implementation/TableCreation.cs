using Cassandra.NetCore.ORM;
using Meetup.WomenInTech.CassandraRepository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meetup.WomenInTech.CassandraRepository.Implementation
{
    public class TableCreation
    {
        private readonly ICassandraDbContext _dbContext;
        public TableCreation(ICassandraDbContext cassandraDb)
        {
            _dbContext = cassandraDb;
        }

        public void CreateKeyspace()
        {
            _dbContext.CreateClusterAsync<UserDetails>().Wait();
        }
    }
}
