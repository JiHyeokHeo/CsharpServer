﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    class Session
    {
        Socket _socket;
        int _disconnected = 0;

        public void Start(Socket socket)
        {
            _socket = socket;
            SocketAsyncEventArgs recvArges = new SocketAsyncEventArgs(); ;
            recvArges.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceiveCompleted);
            recvArges.SetBuffer(new byte[1024], 0, 1024);

            RegisterReceive(recvArges);
        }

        public void Send(byte[] sendBuff)
        {
            //_socket.Send(sendBuff);
            SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
            sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);
            sendArgs.SetBuffer(sendBuff, 0, sendBuff.Length);

            RegisterSend(sendArgs);
        }

        public void Disconnect()
        {
            if (Interlocked.Exchange(ref _disconnected, 1) == 1)
                return;
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        #region 네트워크 통신

        void RegisterSend(SocketAsyncEventArgs args)
        {
            bool pending = _socket.SendAsync(args);
            if (pending == false)
                OnSendCompleted(null, args);
        }

        void OnSendCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                try
                {
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine($"OnSendCompleted Failed {e}");
                }
            }
            else
            {
                Disconnect();
            }
        }

        void RegisterReceive(SocketAsyncEventArgs args)
        {
            // 비동기 Non Blocking Version
            bool pending = _socket.ReceiveAsync(args);
            if (pending == false)
                OnReceiveCompleted(null, args);
        }

        void OnReceiveCompleted(Object sender, SocketAsyncEventArgs args)
        {
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                try
                {
                    string recvData = Encoding.UTF8.GetString(args.Buffer, args.Offset, args.BytesTransferred);
                    Console.WriteLine($"[From Client] {recvData}");
                    RegisterReceive(args);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"OnReceiveCompleted Failed{e}");
                }
            }
            else
            {
                Disconnect();
            }

        }
        #endregion
    }
}
