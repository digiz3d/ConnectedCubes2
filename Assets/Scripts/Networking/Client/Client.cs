using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System;

namespace StupidNetworking
{
    public class Client
    {
        private Thread _clientThread;
        private bool _stopping = false;

        public Client(string serverIp, int serverPort)
        {
            _clientThread = new Thread(() =>
            {
                _stopping = false;

                Debug.Log($"[Client Thread] Connecting to {serverIp}:{serverPort}");

                TcpClient tcpClient = new TcpClient();

                if (!tcpClient.ConnectAsync(serverIp, serverPort).Wait(1000))
                {
                    Debug.Log("[Client Thread] Could not connect");
                    tcpClient.Close();
                    return;
                }

                NetworkStream stream = tcpClient.GetStream();

                while (!_stopping)
                {
                    if (stream.CanRead && stream.DataAvailable)
                    {
                        Debug.Log("client can read");
                        NetworkMessage message = NetworkMessage.ReadFrom(stream);
                        NetworkManager.Singleton.networkReceivedMessages.Enqueue(message);
                    }

                    Debug.Log("[Client Thread] I'm alive !");
                    Thread.Sleep(1000);
                }

                tcpClient.Close();

                _stopping = false;
            })
            {
                IsBackground = true,
            };

            _clientThread.Start();
        }

        public void Stop()
        {
            _stopping = true;
            _clientThread.Join();
            _clientThread = null;
        }
    }
}