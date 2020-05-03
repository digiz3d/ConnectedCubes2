using System;
using System.Net.Sockets;

public class NetworkMessage
{
    public NetworkMessageType NetworkMessageType { internal set; get; }
    public byte SenderClientId { internal set; get; }
    public byte[] Data { internal set; get; }

    public static NetworkMessage ReadFrom(NetworkStream stream)
    {
        byte[] headerBuffer = new byte[2 * sizeof(byte) + sizeof(int)];
        stream.Read(headerBuffer, 0, 2 * sizeof(byte) + sizeof(int));

        NetworkMessageType networkMessageType = (NetworkMessageType)headerBuffer[0];
        byte clientId = headerBuffer[1];

        int dataLength = BitConverter.ToInt32(headerBuffer, 2 * sizeof(byte));
        byte[] data = new byte[dataLength];
        stream.Read(data, 0, dataLength);

        return new NetworkMessage(networkMessageType, clientId, data);
    }

    public NetworkMessage(NetworkMessageType networkMessageType, byte senderClientId, byte[] data)
    {
        NetworkMessageType = networkMessageType;
        SenderClientId = senderClientId;
        Data = data;
    }
}

public enum NetworkMessageType : byte
{
    RPC,
    Ping,
}