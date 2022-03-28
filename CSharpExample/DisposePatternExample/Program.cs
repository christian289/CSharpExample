using System;
using System.Diagnostics;
using System.Threading;

namespace DisposePatternExample
{
    // 종료자는 GC가 가비지를 수집할 때 발생하는 특수한 메서드다.
    // 종료자가 구현된 클래스는 다른 가비지들보다 늦게 제거된다.
    // 가비지 컬렉션의 시점을 알 수 없기 때문에 종료자의 호출은 보장할 수 없다.
    // 따라서 종료자를 구현하기 보다는 Dispose Pattern을 이용하여 객체를 미리미리 제거하면 GC를 쓰지 않고 제거할 수 있다. (task 3)
    // 이 코드는 https://www.udemy.com/course/csharp-memory-tricks-learn-how-to-master-the-garbage-collector/ 의 예제입니다.
    class DisposeClass : IDisposable
    {
        private Stopwatch stopwatch = null;
        private bool disposed = false;

        public DisposeClass()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void DoWork()
        {
            double j = 0;

            for (int i = 0; i < 1000; i++)
            {
                j += i + i;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // 이 객체에 대해 Finalizer 호출 안하도록 함. (Finalizer 호출하면 SOH GC 1세대로 넘어가서 Finalizer가 호출되기 때문에 바로 가비지가 제거되지 않음.)
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                stopwatch.Stop();
                Interlocked.Increment(ref Program.FinalisedObjects);
                Interlocked.Add(ref Program.TotalTime, stopwatch.ElapsedMilliseconds);

                if (disposing)
                {
                    // explicitly called from user code
                }
                else
                {
                    // called from finaliser
                }

                disposed = true;
            }
        }

        ~DisposeClass()
        {
            Dispose(false);
        }
    }
    class NotDisposeClass
    {
        private Stopwatch stopwatch = null;

        public NotDisposeClass()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void DoWork()
        {
            double j = 0;

            for (int i = 0; i < 1000; i++)
            {
                j += i * i;
            }
        }

        ~NotDisposeClass() // 종료자(Finalizer)
        {
            stopwatch.Stop();
            Interlocked.Increment(ref Program.FinalisedObjects);
            Interlocked.Add(ref Program.TotalTime, stopwatch.ElapsedMilliseconds);
        }
    }

    class Program
    {
        public static long FinalisedObjects = 0;
        public static long TotalTime = 0;

        static void Main(string[] args)
        {
            for (int i = 0; i < 500000; i++)
            {
                // task 1
                // Number of disposed/finalised objects: 391179k
                // Average resource lifetime: 296.1393965422479ms
                //var obj = new WithoutDispose();
                //obj.DoWork();

                // task 2
                // Number of disposed/finalised objects: 445848k
                // Average resource lifetime: 262.96782535752095ms 
                //var obj = new WithDispose();
                //obj.DoWork();

                // task 3
                // Number of disposed/finalised objects: 500000k
                // Average resource lifetime: 1.2E-05ms 
                using var obj = new DisposeClass();
                obj.DoWork();
            }

            double avgLifetime = 1.0 * TotalTime / FinalisedObjects;
            Console.WriteLine("Number of disposed/finalised objects: {0}k", FinalisedObjects);
            Console.WriteLine("Average resource lifetime: {0}ms", avgLifetime);
        }
    }
}
