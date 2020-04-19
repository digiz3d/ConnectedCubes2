using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StupidNetworking
{
    public class Client
    {
        public Client(string serverIp, int serverPort)
        {
            Debug.Log($"creating a network client {serverIp} {serverPort}");
        }
    }
}