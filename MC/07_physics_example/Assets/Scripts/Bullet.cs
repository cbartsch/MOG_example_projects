using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start() {
        Invoke(nameof(Delete), 3);
    }

    private void Delete() {
        //destroy bullet after some time
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //handle object being hit by bullet
        Debug.Log("bullet collided with:" + collision.collider.name);

        //destroy bullet afterwards
        Destroy(gameObject);
    }
}
