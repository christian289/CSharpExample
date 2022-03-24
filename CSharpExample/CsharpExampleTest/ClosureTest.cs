using System;
using Xunit;
using Xunit.Abstractions;

namespace CsharpExampleTest
{
    public class ClosureTest
    {
        private readonly ITestOutputHelper output;

        public ClosureTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test1()
        {
            Func<int>[] funArr = new Func<int>[5];

            for (int i = 0; i < 5; i++)
            {
                funArr[i] = () => i;
            }

            foreach (Func<int> fun in funArr)
            {
                output.WriteLine(fun().ToString());
            }
        }

        [Fact]
        public void Test2()
        {
            int i = default;
            Func<int>[] funArr = new Func<int>[5] { () => i, () => i, () => i, () => i, () => i };

            foreach (Func<int> item in funArr)
            {
                i++;
            }

            foreach (Func<int> fun in funArr)
            {
                output.WriteLine(fun().ToString());
            }
        }
    }
}
