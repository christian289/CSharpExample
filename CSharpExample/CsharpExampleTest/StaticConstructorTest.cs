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
        public static string TestString2 { get; set; } = "gggg"; // ILSpy로 확인하면 아래 생성자를 주석처리하더라도 빌드 시 IL이 생성되면서 정적생성자가 생성되고, 이 "gggg"를 정적생성자 안에서 초기화되도록 되어있다.

        static StaticTestClass()
        {
            TestString = "정적 생성자의 호출 타이밍 확인하시오";
        }
    }
}
