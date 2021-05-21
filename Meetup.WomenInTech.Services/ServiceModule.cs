using Meetup.WomenInTech.Services.Implementation;
using Meetup.WomenInTech.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Meetup.WomenInTech.Services
{
    public class ServiceModule
    {
        public ServiceModule(IServiceCollection services)
        {
            services.AddTransient<IEventGridService, EventGridService>();
            services.AddTransient<IBlobStorageService, BlobStorageService>();
            services.AddTransient<ICassandraService, CassandraService>();
        }
    }
}
