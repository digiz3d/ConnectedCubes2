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
        private List<ServerClient> serverClientsList;

        private Thread _serverThread;
        private bool _stoping = false;

        public Server(int port)
        {
            serverClientsList = new List<ServerClient>();

            _serverThread = new Thread(() =>
            {
                _stoping = false;
                Debug.Log("[Server Thread] Hi.");

                TcpListener listener = TcpListener.Create(port);

                while (!_stoping)
                {
                    while (listener.Pending())
                    {
                        Debug.Log("[Server Thread] Accepting new client.");
                        TcpClient tcpClient = listener.AcceptTcpClient();
                        try
                        {
                            byte newClientId = ClientIdsManager.CreateId();
                            serverClientsList.Add(new ServerClient(newClientId, tcpClient));
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.Message);
                        }
                    }

                    foreach(ServerClient client in serverClientsList)
                    {
                        // TODO
                    }

                    Debug.Log("[Server Thread] I'm alive !");
                    Thread.Sleep(1000);
                }

                listener.Stop();
                _stoping = false;

            })
            {
                IsBackground = true,
                Priority = System.Threading.ThreadPriority.Highest
            };

            _serverThread.Start();
        }

        public void Stop()
        {
            _stoping = true;
        }
    }
}