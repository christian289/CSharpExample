using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using DxMvvmWpfDataTemplateNavigationExample.ViewModels;
using DxMvvmWpfDataTemplateNavigationExample.Views;

namespace DxMvvmWpfDataTemplateNavigationExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly IHost host;

        public App()
        {
            Messenger.Default = new Messenger(isMultiThreadSafe: true, actionReferenceType: ActionReferenceType.WeakReference);

            host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //IHostEnvironment env = hostingContext.HostingEnvironment;
                    //config.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    //config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, service) =>
                {
                    service.AddTransient(ViewModelSource.GetPOCOType(typeof(SubView1ViewModel)));
                    service.AddTransient(ViewModelSource.GetPOCOType(typeof(SubView2ViewModel)));
                    service.AddTransient(ViewModelSource.GetPOCOType(typeof(SubView3ViewModel)));
                    service.AddTransient(ViewModelSource.GetPOCOType(typeof(MainWindowViewModel)));
                    service.AddTransient<SubView1>();
                    service.AddTransient<SubView2>();
                    service.AddTransient<SubView3>();
                    service.AddTransient<MainWindow>();
                })
                .Build();

            ServiceProvider = host.Services;
        }

        public static IServiceProvider ServiceProvider { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
