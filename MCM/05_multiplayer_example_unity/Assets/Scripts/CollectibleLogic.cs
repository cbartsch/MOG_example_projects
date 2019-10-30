using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CollectibleLogic : MonoBehaviour {
    // Use this for initialization
    void Start() { 
    }

    // Update is called once per frame
    void Update() {
    }

    void OnTriggerEnter(Collider other) {
        //only apply collision logic on server, sync events to clients
        if (!NetworkServer.active) {
            return;
        }

        var player = other.GetComponent<PlayerLogic>();

        //collided with player
        if (player != null) {
            player.Points++;
            Destroy(gameObject);
        }
    }
}