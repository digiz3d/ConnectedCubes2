using UnityEngine;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace StupidNetworking
{
    public class Server
    {
        private Thread _listenerThread;

        public Server(int serverPort)
        {
            Debug.Log($"creating a network client {serverPort}");
            _listenerThread = new Thread(() => { Listen(serverPort); })
            {
                IsBackground = true
            };
            _listenerThread.Start();
        }

        private void Listen(int port) {
            TcpListener listener = TcpListener.Create(port);
        }
    }
}