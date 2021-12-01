using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Unity.Netcode;
using NetworkManager = Unity.Netcode.NetworkManager;

public class GameLogic : MonoBehaviour {
    public GameObject collectiblePrefab;

    public GameObject gameWorld;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        //logic here only performed on server
        if (!NetworkManager.Singleton.IsServer) {
            return;
        }

        var collectible = FindObjectOfType<CollectibleLogic>();

        //spawn collectible at random X/Z pos if none exists
        if (collectible == null) {
            float maxPos = 7.5f;
            
            float xPos = Random.Range(-maxPos, maxPos);
            float yPos = -5;
            float zPos = Random.Range(-maxPos, maxPos);

            var newInstance = Instantiate(
                collectiblePrefab, //template
                new Vector3(xPos, yPos, zPos),
                Quaternion.identity, //rotation
                gameWorld.transform //parent
            ); 

            newInstance.GetComponent<NetworkObject>().Spawn();
        }
    }
}