using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuartzDotnetAndGenericHost.Jobs;
using Quartz;
using QuartzDotnetAndGenericHost.Models.Settings;
using System.Net.Http;
using QuartzDotnetAndGenericHost.Models.DAO;
using Microsoft.EntityFrameworkCore;
using QuartzDotnetAndGenericHost.Services;

namespace QuartzDotnetAndGenericHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder rtnValue = Host.CreateDefaultBuilder(args)
                .UseConsoleLifetime()
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.Sources.Clear();

                    IHostEnvironment env = hostingContext.HostingEnvironment;

                    configuration.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
#if DEBUG
                    configuration.AddJsonFile("appsettings.develop.json", optional: true, reloadOnChange: true);
#elif !DEBUG
                    configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
#endif
                    IConfigurationRoot configurationRoot = configuration.Build();
                    MySettings setting = new();
                    configurationRoot.GetSection(nameof(MySettings)).Bind(setting);
                })
                .ConfigureServices((context, services) =>
                {
                    // appsettings.json Setting
                    services.Configure<MySettings>(context.Configuration.GetSection(nameof(MySettings)));
#if DEBUG
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionJobFactory();
                        JobKey jobKey = new(nameof(DefaultJob));
                        q.AddJob<DefaultJob>(j => j.WithIdentity(jobKey));
                        q.AddTrigger(t => t
                            .ForJob(jobKey)
                            .WithIdentity($"{nameof(DefaultJob)}_1")
                            .WithCronSchedule("0/10 * * * * ?")); // 10초마다 동작
                        q.AddTrigger(t => t
                            .ForJob(jobKey)
                            .WithIdentity($"{nameof(DefaultJob)}_1")
                            .WithCronSchedule("0/3 * 0-9 * * ?")); // 0시 ~ 9시 사이에 2초마다 동작
                        q.AddTrigger(t => t
                            .ForJob(jobKey)
                            .WithIdentity($"{nameof(DefaultJob)}_2")
                            .WithCronSchedule("0/3 * 12-13 * * ?")); // 12시 ~ 13시 사이에 2초마다 동작
                        q.AddTrigger(t => t
                            .ForJob(jobKey)
                            .WithIdentity($"{nameof(DefaultJob)}_3")
                            .WithCronSchedule("0/3 * 19-20 * * ?")); // 19시 ~ 20시 사이에 2초마다 동작
                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
#elif !DEBUG
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionJobFactory();
                        JobKey jobKey = new(nameof(DefaultJob));
                        q.AddJob<DefaultJob>(j => j.WithIdentity(jobKey));
                        q.AddTrigger(t => t
                            .ForJob(jobKey)
                            .WithIdentity($"{nameof(DefaultJob)}_1")
                            .WithCronSchedule("0/3 * 0-9 * * ?")); // 0시 ~ 9시 사이에 2초마다 동작
                        q.AddTrigger(t => t
                            .ForJob(jobKey)
                            .WithIdentity($"{nameof(DefaultJob)}_2")
                            .WithCronSchedule("0/3 * 12-13 * * ?")); // 12시 ~ 13시 사이에 2초마다 동작
                        q.AddTrigger(t => t
                            .ForJob(jobKey)
                            .WithIdentity($"{nameof(DefaultJob)}_3")
                            .WithCronSchedule("0/3 * 19-20 * * ?")); // 19시 ~ 20시 사이에 2초마다 동작
                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
#endif
                    // Add My EF Core DbContext
                    services.AddDbContext<MyContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("MyDataBase")));

                    // Add My Class
                    CookieContainer cookieContainer = new();
                    services.AddSingleton(_ => cookieContainer);
                    services.AddScoped<MyService>();

                    // Add My HttpClientFactory
                    services.AddHttpClient();
                    services.AddHttpClient("CookieContainerHttpClient").ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        HttpClientHandler handler = new()
                        {
                            UseCookies = true,
                            CookieContainer = cookieContainer,
                        };

                        return handler;
                    });
                });

            return rtnValue;
        }
    }
}
