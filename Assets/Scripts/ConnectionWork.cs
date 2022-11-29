using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class ConnectionWork : NetworkBehaviour
{
    [SerializeField] private TMP_InputField addressField;

    [SerializeField] private GameObject _networkManager;
    
    private UnityTransport _transport;

    private void Start()
    {
        
        _transport = _networkManager.GetComponent<UnityTransport>();
    }

    private void Update()
    {
        _transport.ConnectionData.Address = addressField.text;
    }
}
