using System;
using System.Threading;

namespace ThreadEventWaitHandleTest
{
    class Program
    {
        static bool threadStarted = false;
        static bool threadRunning = false;
        static bool threadExited = false;

        static void Main(string[] args)
        {
            EventWaitHandle started = new EventWaitHandle(false, EventResetMode.ManualReset);
            EventWaitHandle running = new EventWaitHandle(false, EventResetMode.ManualReset);
            EventWaitHandle exited = new EventWaitHandle(false, EventResetMode.ManualReset);
            Thread t = new Thread(threadFunc);

            t.Start((started, running, exited));

            started.WaitOne();

            Console.WriteLine($"{nameof(threadStarted)} 통과");

            running.WaitOne();

            Console.WriteLine($"{nameof(threadRunning)} 통과");

            exited.WaitOne();

            Console.WriteLine($"{nameof(threadExited)} 통과");
        }

        private static void threadFunc(object obj)
        {
            var events = (ValueTuple<EventWaitHandle, EventWaitHandle, EventWaitHandle>)obj;

            Console.WriteLine($"{nameof(threadStarted)}에 Set");
            events.Item1.Set();

            Console.WriteLine("TEST: " + obj);

            Thread.Sleep(3000);

            for (int i = 0; i < 10; i++)
            {
                events.Item2.Set();
                Thread.Sleep(100);
            }

            events.Item3.Set();
        }
    }
}
