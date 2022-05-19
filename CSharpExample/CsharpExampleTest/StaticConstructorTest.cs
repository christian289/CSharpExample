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

    // https://stackoverflow.com/questions/7400438/why-this-static-constructor-is-not-getting-called
    // 위 문서에 따르면 const일 경우 정적 생성자가 호출되지 않는다.
    // 그런데 어찌보면 당연하다. const는 컴파일하면서 모두 코드상에 초기화하는 값으로 모두 변환되고 컴파일 된다. 인스턴스라는게 애초에 존재하지 않고 value만 있는 게 const다.
}
