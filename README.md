# ACRCloudApiSdkCore

![build](https://github.com/musmanrafiq/ACRCloudApiSdkCore/workflows/build/badge.svg)

![tests](https://github.com/musmanrafiq/ACRCloudApiSdkCore/workflows/tests/badge.svg?branch=master)

![nuget](https://github.com/musmanrafiq/ACRCloudApiSdkCore/workflows/nuget/badge.svg?branch=master)

[![NuGet Package](https://img.shields.io/nuget/v/AcrCloudApiSdk.svg)](https://www.nuget.org/packages/AcrCloudApiSdk)

AcrCloud Api usage integrated inside this library to access AcrCloud exposed library, just provide your keys and consume its exposed public api through provided methods.
For example, lets consume that library in .net core 3.1 project.

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
    var acrCloudService = new AcrCloudConsoleService(acrSettings);

    //now here we can call all of provided functions
