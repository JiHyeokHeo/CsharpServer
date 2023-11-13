using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        volatile static bool _stop = false;

        static void ThreadMain()
        {
            Console.WriteLine("쓰레드 시작!");

            // C++ 에서 volatile 키워드는 C#과 조금 다르다
            // C++ 에서도 코드 최적화 내용도 있다.
            // C#에서는 volatile 키워드보다 다른 방식을 사용하는걸 권한다.
            // C#에서는 캐시를 무시하고 최신값을 가져와라 라는 뜻도 있다.

            // 릴리즈 모드로 들어가면 아래와 같이 자동 변환을 해준다. 
            // 이로 인해 멀티쓰레드 환경에서 오류가 생긴 이유이다.
            // 이를 해결하기 위한 volatile 이라는 키워드를 사용한다.
            //if(_stop == false)
            //{
            //    while(true)
            //    {

            //    }
            //}

            while (_stop == false)
            {
                // 누군가가 stop 신호를 해주기를 기다린다.
            }

            Console.WriteLine("쓰레드 종료!");
        }

        static void Main(string[] args)
        {
            Task t = new Task(ThreadMain);
            t.Start();

            // 1초동안 슬립 1000ms
            Thread.Sleep(1000);

            _stop = true;

            Console.WriteLine("Stop 호출");
            Console.WriteLine("종료 대기중");
            t.Wait();
            Console.WriteLine("종료 성공");
        }
    }
}