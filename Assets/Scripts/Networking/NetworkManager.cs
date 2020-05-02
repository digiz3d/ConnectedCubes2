using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace StupidNetworking
{
    public class NetworkManager : MonoBehaviour
    {
        public bool IsClient { get; set; }
        public bool IsServer { get; set; }

        public static NetworkManager Singleton { get; set; }

        private Server server;

        private void OnEnable()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Singleton = this;
                DontDestroyOnLoad(gameObject);
                Application.runInBackground = true;
            }
        }

        private void OnDestroy()
        {
            if (Singleton != null && Singleton == this)
            {
                Singleton = null;
            }
        }


        public void StartServer()
        {
            if (IsClient || IsServer) return;

            IsServer = true;
            server = new Server(27015);
        }

        public void StopServer()
        {
            if (!IsServer) return;
            IsServer = false;

            server.Stop();
        }

        public void ConnectToServer()
        {
            if (IsClient || IsServer) return;

        }

        public void DisconnectFromServer()
        {
            if (!IsClient) return;
            IsClient = false;
        }
    }
}