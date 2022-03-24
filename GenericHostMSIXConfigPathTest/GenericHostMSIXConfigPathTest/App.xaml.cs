using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace GenericHostMSIXConfigPathTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost host;
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    IHostEnvironment env = hostingContext.HostingEnvironment;
                    //config.SetBasePath(env.ContentRootPath);
                    config.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)); // MSIX에서는 이렇게 실행경로를 가져와서 해야함.
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    IConfigurationRoot configurationRoot = config.Build();
                    TestOptions testSettings = new();
                    configurationRoot.GetSection(nameof(TestOptions)).Bind(testSettings);
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<TestOptions>(context.Configuration.GetSection(nameof(TestOptions)));
                })
                .Build();

            ServiceProvider = host.Services;
        }
    }
}
