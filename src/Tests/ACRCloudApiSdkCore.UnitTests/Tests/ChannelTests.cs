using AcrCloudApiSdk;
using AcrCloudApiSdk.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ACRCloudApiSdkCore.UnitTests
{
    public class ChannelTests
    {
        private IConfiguration Configuration { get; set; }
        private readonly AcrCloudOptions options;

        public ChannelTests()
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
            options = acrCloudOptions.Value;
        }

        [Fact]
        public async Task IamGetting_List_Of_Channels()
        {
            // arrange
            var acrCloudService = new AcrCloudConsoleService(options.AccountAccessKey,
            options.AccountAccessSecret, options.BaseUrl, options.DatabaseMonitoring.ProjectName);

            // act
            var channels = await acrCloudService.GetArcChannelsAsync();
            var notEmpty = channels.items.Any();

            // assert
            Assert.True(notEmpty);

        }
    }
}
