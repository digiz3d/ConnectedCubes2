using System;
using System.Net;
using System.Net.Sockets;

namespace StupidNetworking
{
    public class ServerClient
    {
        private byte id;
        private TcpClient tcp;
        public NetworkStream NetworkStream { get; internal set; }

        public ServerClient(byte id, TcpClient tcp)
        {
            this.id = id;
            this.tcp = tcp;
            NetworkStream = tcp.GetStream();
        }
    }
}