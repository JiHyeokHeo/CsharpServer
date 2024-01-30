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
        // RWLock ReaderWriteLock
        static ReaderWriterLockSlim _lock3 = new ReaderWriterLockSlim();


        // 직접 만든다.

        // 운영 보상이 주에 1번만 할텐데 lock을 하면 아쉽다.
        // [] [] [] [] []
        class Reward
        {

        }


        // 99.999 
        static Reward GetRewardById(int id)
        {
            _lock3.EnterReadLock();

            _lock3.ExitReadLock();
            return null;
        }

        // 0.001%
        static void AddReward(Reward reward)
        {
            _lock3.EnterWriteLock();

            _lock3.ExitWriteLock();
        }

        static void Main(string[] args)
        {
            lock(_lock)
            {

            }
            

        }
    }
}