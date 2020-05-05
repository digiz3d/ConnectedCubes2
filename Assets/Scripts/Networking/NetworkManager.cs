using UnityEngine;
using System.Collections.Generic;

namespace StupidNetworking
{
    public class NetworkManager : MonoBehaviour
    {
        public bool IsClient { get; internal set; }
        public bool IsServer { get; internal set; }

        public static NetworkManager Singleton { get; internal set; }

        private Server server;
        private Client client;
        private byte clientId = 0;

        public Queue<NetworkMessage> networkReceivedMessages = new Queue<NetworkMessage>();

        private List<NetworkedObject> spawnedNetworkedObjects = new List<NetworkedObject>();

        private void Update()
        {
            while (networkReceivedMessages.Count > 0)
            {
                HandleNetworkMessages(networkReceivedMessages.Dequeue());
            }
        }

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

        private void HandleNetworkMessages(NetworkMessage msg)
        {
            switch (msg.NetworkMessageType)
            {
                case NetworkMessageType.GIVE_CLIENT_ID:
                    SetClientId(msg);
                    break;

            }
        }

        public void StartServer()
        {
            if (IsClient || IsServer) return;

            Init();

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
            if (IsClient) return;
            IsClient = true;

            Init();

            client = new Client("127.0.0.1", 27015);
        }

        public void DisconnectFromServer()
        {
            if (!IsClient) return;
            IsClient = false;

            client.Stop();
        }

        private void Init()
        {
            spawnedNetworkedObjects.Clear();
        }

        private void SpawnNetworkedObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject go = Instantiate(prefab, position, rotation);
            spawnedNetworkedObjects.Add(go.GetComponent<NetworkedObject>());
        }

        private void SetClientId(NetworkMessage msg)
        {
            clientId = (byte)msg.Stream.ReadByte();

            Debug.Log($"My client id is now : {clientId}");
        }
    }
}