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
        public void Test()
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
    }
}
