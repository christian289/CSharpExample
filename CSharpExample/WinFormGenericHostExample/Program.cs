using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;

namespace WindowsFormsGenericHostSample
{
    static class Program
    {
        private static IHost host;

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.Sources.Clear();
                    IHostEnvironment env = context.HostingEnvironment;
                    builder.SetBasePath(context.HostingEnvironment.ContentRootPath);
                    builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                }).ConfigureServices((context, services) =>
                {
                    ConfigureServices(context.Configuration, services);
                }).ConfigureLogging(logging =>
                {

                }).Build();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider services = serviceScope.ServiceProvider;

            try
            {
                Form1 mainForm = services.GetRequiredService<Form1>();
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<OptionClass>(configuration.GetSection(nameof(OptionClass)));
            services.AddSingleton<Form1>();
        }
    }
}
