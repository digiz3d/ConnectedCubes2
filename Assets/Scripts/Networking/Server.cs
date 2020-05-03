using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace StupidNetworking
{
    public class Server
    {
        private List<ServerClient> serverClientsList = new List<ServerClient>();

        private Thread _serverThread;
        private bool _stopping = false;

        public Server(int port)
        {
            _serverThread = new Thread(() =>
            {
                _stopping = false;
                Debug.Log("[Server Thread] Hi.");

                TcpListener listener = new TcpListener(IPAddress.Any, port);

                listener.Start();

                while (!_stopping)
                {
                    while (listener.Pending())
                    {
                        Debug.Log("[Server Thread] Accepting new client.");
                        TcpClient tcpListener = listener.AcceptTcpClient();
                        try
                        {
                            byte newClientId = ClientIdsManager.CreateId();
                            serverClientsList.Add(new ServerClient(newClientId, tcpListener));
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.Message);
                        }
                    }

                    serverClientsList.RemoveAll(client => client == null);

                    serverClientsList.ForEach(client =>
                    {
                        NetworkStream stream = client.NetworkStream;
                        if (stream.CanRead && stream.DataAvailable)
                        {
                            NetworkMessage message = NetworkMessage.ReadFrom(stream);
                            if (message.SenderClientId != client.Id)
                            {
                                Debug.Log("A client sent a message as someone else.");
                                stream.Close();
                                client.Tcp.Close();
                                client.Tcp.Dispose();
                                client = null;
                            }
                        }
                    });

                    Debug.Log("[Server Thread] I'm alive !");
                    Thread.Sleep(1000);
                }

                listener.Stop();
                _stopping = false;
            })
            {
                IsBackground = true,
            };

            _serverThread.Start();
        }

        public void Stop()
        {
            _stopping = true;
            _serverThread.Join();
            _serverThread = null;
            serverClientsList.Clear();
        }
    }
}