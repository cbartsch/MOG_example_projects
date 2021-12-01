using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;

public class UdpMatchmaker : MonoBehaviour {

    public UNetTransport networkTransport;
    
    //some data so the receiver can recognize the message
    static readonly byte[] MSG = {1, 2, 3, 4, 5, 6, 7, 8};

    //UDP port for matchmaking
    public short matchmakingPort = 1235;

    //port to connect UNet to (same as NetworkServer port setting)
    public short gamePort = 1234;

    //one UDP socket for broadcasting, one for receiving
    private UdpClient client, server;

    //timer to broadcast once per second
    private float timeUntilMsg = 0f;

    //server address when received broadcast message
    private IPEndPoint foundServerEndPoint;
    private bool receiving = false;

    // Use this for initialization
    void Start() {
        server = new UdpClient();
    }

    // Update is called once per frame
    void Update() {
        if (NetworkManager.Singleton.IsServer) {
            //server: send broadcast message to all clients in the local network 
            if (client != null) {
                //do not keep port open
                client.Close();
                client = null;
            }

            //send message once per second
            if (timeUntilMsg <= 0) {
                server.Send(MSG, MSG.Length, 
                    new IPEndPoint(IPAddress.Broadcast, matchmakingPort));
                timeUntilMsg += 1;
            }

            timeUntilMsg -= Time.deltaTime;
        }
        else if (!receiving) {
            //client: receive broadcast message from any address
            if (client == null) {
                try {
                    client = new UdpClient(new IPEndPoint(IPAddress.Any, matchmakingPort));
                }
                catch (SocketException ex) {
                    //matchmakingPort might be occupied by another matchmaker on this device
                    Debug.LogWarning("UdpMatchmaker: cannot listen on port " + matchmakingPort + ": " + ex, this);
                    return;
                }
            }

            client.BeginReceive(MessageReceived, null);
            receiving = true;
        }

        //needs to be done in the main thread
        if (foundServerEndPoint != null && !NetworkManager.Singleton.IsClient) {
            var addr = foundServerEndPoint.Address.ToString();
            var man = NetworkManager.Singleton;

            networkTransport.ConnectPort = gamePort;
            networkTransport.ConnectAddress = addr;
            man.StartClient();

            foundServerEndPoint = null;
        }
    }

    private void MessageReceived(IAsyncResult ar) {
        //can start receiving new messages now
        receiving = false;

        //this variable will contain the endpoint of the server (thus "ref")
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, matchmakingPort);
        byte[] data = client.EndReceive(ar, ref serverEndPoint);

        //check for our defined message - anyone could send us anything
        if (data != null && data.SequenceEqual(MSG)) {
            foundServerEndPoint = serverEndPoint;
        }
    }
}