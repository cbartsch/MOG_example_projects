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
            float yPos = -5;
            collectible = (Instantiate(
                collectiblePrefab, //template
                new Vector3( //position
                    Random.Range(-maxPos, maxPos), yPos,
                    Random.Range(-maxPos, maxPos)),
                Quaternion.identity, //rotation
                gameWorld.transform //parent
            ) as GameObject).GetComponent<CollectibleLogic>();

            collectible.gameObject.GetComponent<NetworkObject>().Spawn();
        }
    }
}