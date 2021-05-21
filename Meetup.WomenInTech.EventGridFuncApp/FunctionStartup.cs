using Cassandra.NetCore.ORM;
using Meetup.WomenInTech.CassandraRepository.Implementation;
using Meetup.WomenInTech.CassandraRepository.Interface;
using Meetup.WomenInTech.Services.Implementation;
using Meetup.WomenInTech.Services.Interface;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Meetup.WomenInTech.EventGridFuncApp.FunctionStartup))]
namespace Meetup.WomenInTech.EventGridFuncApp
{
    public class FunctionStartup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;

            services.AddScoped<ICassandraService, CassandraService>();
            services.AddScoped<IRepository, Repository>();

            services.AddSingleton<ICassandraDbContext>(c =>
            {
                var username = builder.GetContext().Configuration["Cassandra:Username"];
                var password = builder.GetContext().Configuration["Cassandra:Password"];
                var contactPoint = builder.GetContext().Configuration["Cassandra:CassandraContactPoint"];
                var port = builder.GetContext().Configuration["Cassandra:CassandraPort"];
                var keySpace = builder.GetContext().Configuration["Cassandra:KeySpace"];
                return new CassandraDbContext(username, password, contactPoint, int.Parse(port), keySpace);
            });
        }
    }
}
