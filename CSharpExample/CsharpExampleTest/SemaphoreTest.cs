using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CsharpExampleTest
{
    public class SemaphoreTest
    {
        private readonly ITestOutputHelper output;
        const int size = 10000;

        public SemaphoreTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task Test()
        {
            List<int> sources = new(size);

            for (int i = 0; i < size; i++)
            {
                sources.Add(i);
            }

            SemaphoreSlim semaphoreSlim = new(10);
            List<Task> tasks = new(sources.Count);

            foreach (int item in sources)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await semaphoreSlim.WaitAsync();

                    try
                    {
                        output.WriteLine(item.ToString());
                    }
                    finally
                    {
                        semaphoreSlim.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);
            output.WriteLine("Complete!");
        }

        [Fact]
        public async Task Test2()
        {
            List<int> sources = new(size);

            for (int i = 0; i < size; i++)
            {
                sources.Add(i);
            }

            SemaphoreSlim semaphoreSlim = new(2);
            List<Task> tasks = new(sources.Count);

            foreach (int item in sources)
            {
                await semaphoreSlim.WaitAsync();

                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        output.WriteLine(item.ToString());
                    }
                    finally
                    {
                        semaphoreSlim.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);
            output.WriteLine("Complete!");
        }
    }
}
