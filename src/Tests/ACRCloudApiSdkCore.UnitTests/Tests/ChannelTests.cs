using AcrCloudApiSdk.Helpers;
using AcrCloudApiSdk.Models.Options;
using ACRCloudApiSdkCore.UnitTests.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;
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
           .AddJsonFile("appSettings.json").AddJsonFile("appSettings.Development.json", optional: true);
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
        public void IamGetting_List_Of_Channels()
        {
            //var acrCloudService = new AcrCloudConsoleService(options.AccountAccessKey,
            //options.AccountAccessSecret, options.BaseUrl, options.DatabaseMonitoring.ProjectName);
            //var res = acrCloudService.GetArcChannelsAsync().GetAwaiter().GetResult();

            // arrange
            var fakeResponse = ProduceChannelResponse(Strings.Channel.NoError);

            // act            
            var response = fakeResponse.Transform();

            // assert
            Assert.True(string.IsNullOrEmpty(response.ErrorStatus));
            Assert.True(response.Items.Any());
        }

        [Fact]
        public void I_Got_Protocol_Error()
        {
            //var acrCloudService = new AcrCloudConsoleService(options.AccountAccessKey,
            //options.AccountAccessSecret, options.BaseUrl, options.DatabaseMonitoring.ProjectName);
            //var res = acrCloudService.GetArcChannelsAsync().GetAwaiter().GetResult();

            // arrange
            var fakeResponse = ProduceChannelResponse(Strings.Channel.ProtocolError);

            // act            
            var response = fakeResponse.Transform();

            // assert
            Assert.True(response.ErrorStatus == Strings.Channel.ProtocolError);
        }

        private string ProduceChannelResponse(string errorType)
        {
            var responseString = string.Empty;

            switch (errorType)
            {
                case Strings.Channel.NoError:
                    responseString = "{\"items\":[{\"id\":241165,\"state\":\"Running\",\"stream_name\":\"Hungama - 90's Super Hits\",\"city\":\"Mumbai\",\"country\":\"India\",\"continent\":\"Asia\",\"website\":\"http://www.hungama.com/#/music/live-radio\",\"urls\":[],\"user_defined\":[]},{\"id\":241173,\"state\":\"Running\",\"stream_name\":\"Hindi Retro Hits Radio\",\"city\":\"\",\"country\":\"India\",\"continent\":\"Asia\",\"website\":\"https://gaana.com/radio/mirchiplay-retro-hits\",\"urls\":[],\"user_defined\":[]}],\"_links\":{\"self\":{\"href\":\"https://api.acrcloud.com/v1/acrcloud-monitor-streams?page=1&project_name=BroadcastDatabase1&per_page=50&per-page=50\"}},\"_meta\":{\"totalCount\":2,\"pageCount\":1,\"currentPage\":1,\"perPage\":50}}";
                    break;
                case Strings.Channel.ProtocolError:
                    responseString = "{ \"errorStatus\":\"ProtocolError\",\"error\":\"The remote server returned an error: (406) Not Acceptable.\",\"items\":null,\"_links\":null,\"_meta\":null}";
                    break;
            }
            return responseString;
        }
    }
}
