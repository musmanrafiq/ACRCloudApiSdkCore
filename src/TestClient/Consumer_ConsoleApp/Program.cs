using AcrCloudApiSdk;
using AcrCloudApiSdk.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Consumer_ConsoleApp
{
    public static class Program
    {
        private static IConfiguration Configuration { get; set; }

        static async Task Main(string[] args)
        {
            //load settings from appsettings.json
            IConfigurationBuilder builder = new ConfigurationBuilder()
           .AddJsonFile("appSettings.json").AddJsonFile("appSettings.Development.json");
            Configuration = builder.Build();

            // register services
            IServiceCollection services = new ServiceCollection();
            services.Configure<AcrCloudOptions>(Configuration.GetSection("AcrCloud"));

            // pull registered Services
            var ServiceProvider = services.BuildServiceProvider();
            var acrCloudOptions = ServiceProvider.GetService<IOptions<AcrCloudOptions>>();
            var acrSettings = acrCloudOptions.Value;

            // call get channel report
            var acrCloudService = new AcrCloudConsoleService(acrSettings);
            var output = await acrCloudService.FetchChannelReport("1", DateTime.Now);
            // test any method here
        }
    }
}
