using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //handle object being hit by bullet
        Debug.Log("bullet collided with:" + collision.collider.name);

        //destroy bullet afterwards
        Destroy(gameObject);
    }
}
