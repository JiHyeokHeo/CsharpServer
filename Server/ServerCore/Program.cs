using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    // [ JobQueue ] // TLS 공간에 한방에 많이 뽑아다가 가져다가 쓰면 된다

    class Program
    {
        static ThreadLocal<string> ThreadName = new ThreadLocal<string>(() => { return $"My name Is {Thread.CurrentThread.ManagedThreadId}"; });

        static void WhoAmI()
        {
            bool repeat = ThreadName.IsValueCreated;
            if (repeat)
                Console.WriteLine(ThreadName.Value + "(repeat)");
            else
                Console.WriteLine(ThreadName.Value);

        }

        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(3, 3);
            Parallel.Invoke(WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI);

            ThreadName.Dispose();
        }
    }
}