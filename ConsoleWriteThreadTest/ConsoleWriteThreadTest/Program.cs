using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWriteThreadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var pg = new Program();
            pg.Click();
            Console.Read();
        }

        private async void Click()
        {
            Console.WriteLine($"Click 메서드 진입: {Thread.CurrentThread.ManagedThreadId}");

            await DoSome();

            Console.WriteLine($"Click 메서드 종료: {Thread.CurrentThread.ManagedThreadId}");
        }

        private Task DoSome()
        {
            Console.WriteLine($"DoSome: {Thread.CurrentThread.ManagedThreadId}");

            return Task.Run(() => {
                Thread.Sleep(100);
                Console.WriteLine($"Inner: {Thread.CurrentThread.ManagedThreadId}");
            });
        }
    }
}
