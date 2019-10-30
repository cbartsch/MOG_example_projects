using UnityEngine;

public class MoveWithAxes : MonoBehaviour
{
    public float speed = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //float xInput = Input.GetAxis("Horizontal");
        //float yInput = Input.GetAxis("Vertical");
      
        //this.transform.position += new Vector3(
        //    Time.deltaTime * xInput * speed,    //x axis
        //    Time.deltaTime * yInput * speed,    //y axis
        //    0                                   //z axis
        //);

        //Debug.Log("Moving position with inputs: " + 
        //    xInput + "/" + yInput, this.gameObject);
    }
}
