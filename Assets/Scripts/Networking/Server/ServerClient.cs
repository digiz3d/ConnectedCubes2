using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace StupidNetworking
{
    public class ServerClient
    {
        public byte Id { get; internal set; }
        public TcpClient Tcp { get; internal set; }
        public NetworkStream NetworkStream { get; internal set; }

        public Queue<NetworkMessage> MessagesToSend = new Queue<NetworkMessage>();

        public ServerClient(byte id, TcpClient tcp)
        {
            this.Id = id;
            this.Tcp = tcp;
            NetworkStream = tcp.GetStream();
        }
    }
}