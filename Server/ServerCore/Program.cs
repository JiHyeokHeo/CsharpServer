using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    // 경합 조건

    class Program
    {
        static int number = 0;

        static void Thread_1()
        {
            // atomic = 원자성

            // 집행검 User2 인벤에 넣어라 - ok
            // 집행검 User1 인벤에서 없애라 - fail 

            for (int i = 0; i < 100000; i++)
            {
                // 성능에서 손해를 많이본다.
                Interlocked.Increment(ref number);
            }
        }

        static void Thread_2() 
        {
            for (int i = 0; i < 100000; i++)
            {
                Interlocked.Decrement(ref number); 
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(number);

        }
    }
}