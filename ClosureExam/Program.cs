using System;

namespace ClosureExam
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int>[] funArr = new Func<int>[5];

            for (int i = 0; i < 5; i++)
            {
                funArr[i] = () => i;
            }

            foreach (Func<int> fun in funArr)
            {
                Console.WriteLine(fun());
            }
        }
    }
}
