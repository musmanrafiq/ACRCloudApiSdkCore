﻿using AcrCloudApiSdk;
using AcrCloudApiSdk.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Consumer_ConsoleApp
{
    class Program
    {
        private static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
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
            acrSettings.AccountAccessSecret, acrSettings.BaseUrl, acrSettings.DatabaseMonitoring.ProjectName);
            var channels = acrCloudService.GetArcChannels();

            Console.WriteLine($"Total Channels are : {channels.Count}");
        }
    }
}