using System;
using Xunit;
using Xunit.Abstractions;

namespace CsharpExampleTest
{
    public class StaticConstructorTest
    {
        private readonly ITestOutputHelper output;

        public StaticConstructorTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test1()
        {
            output.WriteLine(StaticTestClass.TestString);
            output.WriteLine(StaticTestClass.TestString2);
        }
    }

    public static class StaticTestClass
    {
        public static string TestString { get; set; }
        public static string TestString2 { get; set; } = "gggg";

        static StaticTestClass()
        {
            TestString = "정적 생성자의 호출 타이밍 확인하시오";
        }
    }
}
