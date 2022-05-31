using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using WpfAddScopedVsAddTransientTest.ViewModels;

namespace WpfAddScopedVsAddTransientTest
{
    public partial class App : Application
    {
        private readonly IHost host;

        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                })
                .ConfigureServices((context, service) =>
                {
                    service.AddScoped(ViewModelSource.GetPOCOType(typeof(AViewModel)));
                    service.AddScoped(ViewModelSource.GetPOCOType(typeof(BViewModel)));
                    service.AddScoped(ViewModelSource.GetPOCOType(typeof(MainWindowViewModel)));
                    //service.AddTransient(ViewModelSource.GetPOCOType(typeof(AViewModel)));
                    //service.AddTransient(ViewModelSource.GetPOCOType(typeof(BViewModel)));
                    //service.AddTransient(ViewModelSource.GetPOCOType(typeof(MainWindowViewModel)));
                    service.AddTransient<MainWindow>();
                })
                .Build();

            ServiceProvider = host.Services;
        }
    }
}
