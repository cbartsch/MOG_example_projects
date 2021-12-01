using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;

public class NetworkManagerUI : MonoBehaviour {
    public NetworkManager networkManager;
    public UNetTransport networkTransport;

    public InputField inputHostName;

    public Button buttonHost,
        buttonServer,
        buttonClient,
        buttonDisconnect;

    public short port;

    void Update() {
        updateUI();
    }

    public void StartHost() {
        networkTransport.ConnectPort = port;
        networkManager.StartHost();
    }

    public void StartServer() {
        networkTransport.ConnectPort = port;
        networkManager.StartServer();
    }

    public void StartClient() {
        networkTransport.ConnectPort = port;
        networkTransport.ConnectAddress = inputHostName.text;
        networkManager.StartClient();
    }

    private void updateUI() {
        bool networkActive = networkManager.IsServer || networkManager.IsClient;

        buttonHost.gameObject.SetActive(!networkActive);
        buttonServer.gameObject.SetActive(!networkActive);
        buttonClient.gameObject.SetActive(!networkActive);
        inputHostName.gameObject.SetActive(!networkActive);

        buttonDisconnect.gameObject.SetActive(networkActive);
    }

    public void Disconnect() {
        if (networkManager.IsClient || networkManager.IsServer) {
            networkManager.Shutdown();
        }
    }
}