using UnityEngine;

public class UI_STARTUP : MonoBehaviour
{
    public GameObject client;
    public GameObject server;

    // The method for launching the server is needed to call through a button in the interface
    public void StartServer()
    {
        server.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    // The method for launching the client is needed to call through a button in the interface
    public void StartClient()
    {
        client.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
