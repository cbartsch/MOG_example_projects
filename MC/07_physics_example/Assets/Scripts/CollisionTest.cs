using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour {

    public Vector3 initialPosition = new Vector3(0, 10, 0);


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision enter with:" + collision.collider.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("collision exit with:" + collision.collider.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter with:" + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger exit with:" + other.name);

        if(other.name == "WorldTrigger")
        {
            //reset position
            transform.position = initialPosition;

            //reset velocity
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

}
