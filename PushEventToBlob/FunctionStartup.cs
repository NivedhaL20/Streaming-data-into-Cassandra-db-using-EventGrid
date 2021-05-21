using Meetup.WomenInTech.Services.Implementation;
using Meetup.WomenInTech.Services.Interface;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Meetup.WomenInTech.PushEventToBlobFuncApp.FunctionStartup))]
namespace Meetup.WomenInTech.PushEventToBlobFuncApp
{
    public class FunctionStartup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;

            services.AddScoped<IBlobStorageService, BlobStorageService>();
        }
    }
}
