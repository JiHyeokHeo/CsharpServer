using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;
using System.Linq;
using System.Threading.Tasks;

namespace DummyClient
{
    class ServerSession : PacketSession
    {
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override void OnReceivePacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnReceivePacket(this, buffer);
        }

        // 이동 패킷 ((3,2)~~ 좌표로 이동하고 싶다!)
        // 15 3 2
        public override void OnSend(int numofBytes)
        {
            //Console.WriteLine($"Transfered bytes : {numofBytes}");
        }
    }

}
