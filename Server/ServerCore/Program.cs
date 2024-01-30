using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        // a. 근성 -> 스핀락은 무한히 시도하(a번)다가 답이 없으면 양보(b번)를 시도하는식으로 구현되어있다.
        // b. 양보
        // c. 갑질

        // 상호배제
        // Monitor 
        static object _lock = new object();
        static SpinLock _lock2 = new SpinLock();
        // 직접 만든다.

        // 게임 개발하는데에 있어 필요없음
        static Mutex _lock3 = new Mutex();  // 커널모드를 사용하기에 많이 무겁다 // 별도의 프로그램끼리도 동기화 작업을 할때 사용
        
        // 컨텐츠 구현 시
        // 심리스 RPG를 만들때는 멀티스레드가 필요로 하다 
        static void Main(string[] args)
        {
            lock(_lock)
            {

            }
            
            // 스핀락 사용
            //bool lockTaken = false;
            //try
            //{
            //    _lock2.Enter(ref lockTaken);
            //}
            //finally
            //{
            //    if(lockTaken)
            //        _lock2.Exit();
            //}

        }
    }
}