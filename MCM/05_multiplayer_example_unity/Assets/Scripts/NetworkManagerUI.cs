using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class NetworkManagerUI : MonoBehaviour {
    public NetworkManager networkManager;

    public InputField inputHostName;

    public Button buttonHost,
        buttonServer,
        buttonClient,
        buttonDisconnect;

    public short port;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        updateUI();
    }

    public void StartHost() {
        networkManager.networkPort = port;
        networkManager.StartHost();
    }

    public void StartServer() {
        networkManager.networkPort = port;
        networkManager.StartServer();
    }

    public void StartClient() {
        networkManager.networkPort = port;
        networkManager.networkAddress = inputHostName.text;
        networkManager.StartClient();
    }

    private void updateUI() {
        bool networkActive = NetworkServer.active || NetworkClient.active;

        buttonHost.gameObject.SetActive(!networkActive);
        buttonServer.gameObject.SetActive(!networkActive);
        buttonClient.gameObject.SetActive(!networkActive);
        inputHostName.gameObject.SetActive(!networkActive);

        buttonDisconnect.gameObject.SetActive(networkActive);
    }

    public void Disconnect() {
        if (NetworkClient.active) {
            networkManager.StopClient();
        }

        if (NetworkServer.active) {
            networkManager.StopServer();
        }
    }
}