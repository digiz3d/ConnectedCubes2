using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public bool IsClient { get; set; }
    public bool IsServer { get; set; }

    public static NetworkManager Singleton { get; set; }

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
        if (IsServer) return;
    }

    public void StopServer()
    {
        if (!IsServer) return;
        IsServer = false;
    }

    public void ConnectToServer()
    {
        if (IsClient) return;
    }

    public void DisconnectFromServer()
    {
        if (!IsClient) return;
        IsClient = false;
    }
}
