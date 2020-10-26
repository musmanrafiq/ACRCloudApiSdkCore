using AcrCloudApiSdk;
using AcrCloudApiSdk.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Consumer_ConsoleApp
{
    class Program
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

            // call get channels method
            var acrCloudService = new AcrCloudConsoleService(acrSettings.AccountAccessKey,
            acrSettings.AccountAccessSecret, acrSettings.BaseUrl, acrSettings.DatabaseMonitoring.ProjectName, acrSettings.BucketName);
            var response = await acrCloudService.Upload("dddd", "ddd", "a.mp3", "mp3");
            // test any method here
        }
    }
}
