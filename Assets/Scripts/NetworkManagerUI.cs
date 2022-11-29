using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class NetworkManagerUI : MonoBehaviour
{
    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
    }
    
    public void ClientButton()
    {
        NetworkManager.Singleton.StartClient();
    }
}
