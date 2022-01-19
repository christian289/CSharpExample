using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzDotnetAndGenericHost.Jobs
{
    [DisallowConcurrentExecution] // 중복 실행 방지
    internal class DefaultJob : IJob
    {
        const int SEMAPHORE_COUNT = 3;

        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient httpClient;

        public DefaultJob(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpClient = httpClientFactory.CreateClient("CookieContainerHttpClient");
            httpClient.DefaultRequestVersion = HttpVersion.Version20;
            httpClient.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now} {context.Trigger.Key.Name} 타이머 시작!");
            IEnumerable<int> list = Enumerable.Range(0, 10);
            SemaphoreSlim semaphoreSlim = new(SEMAPHORE_COUNT);
            List<Task> tasks = new(list.Count());

            foreach (int item in list)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        await semaphoreSlim.WaitAsync();
                        Console.WriteLine(item);
                    }
                    finally
                    {
                        semaphoreSlim.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}
