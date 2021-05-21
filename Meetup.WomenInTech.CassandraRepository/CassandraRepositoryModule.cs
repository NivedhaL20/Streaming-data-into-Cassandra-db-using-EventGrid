using Meetup.WomenInTech.CassandraRepository.Implementation;
using Meetup.WomenInTech.CassandraRepository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Meetup.WomenInTech.CassandraRepository
{
    public class CassandraRepositoryModule
    {
        public CassandraRepositoryModule(IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddSingleton<TableCreation>();
        }
    }
}
