using UnityEngine;
using UnityEngine.UI;

public class GameStatusManager : MonoBehaviour
{
    public Text textServer;
    public Text textClient;
    public Text textHost;
    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.S))
            ToggleServer();

        if (Input.GetKeyUp(KeyCode.C))
            ToggleClient();

        NetworkManager n = NetworkManager.Singleton;
        if (n == null) return;

        textServer.text = "IsServer = " + (n.IsServer);
        textClient.text = "IsClient =" + (n.IsClient);
        textHost.text = "IsHost =" + (n.IsServer && n.IsClient);
    }

    private void ToggleServer()
    {
        NetworkManager n = NetworkManager.Singleton;

        if (n == null) return;

        if (n.IsServer)
        {
            n.StopServer();
        }
        else
        {
            n.StartServer();
        }
    }

    private void ToggleClient()
    {
        NetworkManager n = NetworkManager.Singleton;

        if (n == null) return;

        if(n.IsClient)
        {
            n.DisconnectFromServer();
        }
        else
        {
            n.ConnectToServer();
        }
    }
}
