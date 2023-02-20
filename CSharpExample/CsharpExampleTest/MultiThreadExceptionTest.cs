using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CsharpExampleTest
{
    public class MultiThreadExceptionTest
    {
        private readonly ITestOutputHelper output;

        public MultiThreadExceptionTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void Test1()
        {
            List<Task> tasks = new();

            try
            {
                foreach (int item in Enumerable.Range(1, 10))
                {
                    tasks.Add(Task.Run(() =>
                    {
                        if (item == 1)
                        {
                            throw new Exception("1 Exception");
                        }

                        output.WriteLine(item.ToString());
                    }));
                }
            }
            catch (Exception ex)
            {
                output.WriteLine(ex.Message);
            }

            await Task.WhenAll(tasks);
        }

        [Fact]
        public async void Test2()
        {
            List<Task> tasks = new();

            foreach (int item in Enumerable.Range(1, 10))
            {
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        if (item == 1)
                        {
                            throw new Exception("1 Exception");
                        }

                        output.WriteLine(item.ToString());
                    }
                    catch (Exception ex)
                    {
                        output.WriteLine(ex.Message);
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}
