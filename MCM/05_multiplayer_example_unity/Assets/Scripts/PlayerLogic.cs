using UnityEngine;
using System.Collections;
using Unity.Netcode;
using Unity.Netcode.Components;

public class PlayerLogic : NetworkBehaviour {

    public float moveSpeed = 10, rotateSpeed = 4;

    //synchronize points and color from server to all clients
    private NetworkVariable<int> points;
    private NetworkVariable<Color> color;

    //public accessor
    public int Points
    {
        get { return points.Value; }
        set { points.Value = value; }
    }

	// Use this for initialization
	void Start ()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            //server "chooses" color for players (SyncVars sync from server to client)
            color.Value = Random.ColorHSV(
                0, 1,
                0.5f, 1, 
                0.5f, 1); 
            //generates colors that are neither too dark nor too bright
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Renderer>().material.color = this.color.Value;

        Debug.Log("player " + name + " is local " + IsLocalPlayer + " is owner " + IsOwner, gameObject);
        
        //only control local player object
        if (IsLocalPlayer)
        {
            control();

            //attach main camera to local player and a few units back (3rd person view)
            Camera.main.transform.parent = transform;
            Camera.main.transform.localPosition = Vector3.forward * -2;
            Camera.main.transform.localRotation = Quaternion.identity;
        }

        GetComponentInChildren<TextMesh>().text = System.Convert.ToString(points.Value);
    }

    public override void OnDestroy()
    {
        //make camera global again instead of following player

        var camera = GetComponentInChildren<Camera>();
        if (camera != null)
        {
            Debug.Log("reset camera", gameObject);
            camera.transform.parent = null;
            camera.enabled = true;
            camera.transform.position = new Vector3(0, 1, -10);
            camera.transform.rotation = Quaternion.Euler(45, 0, 0);
        }
        
        base.OnDestroy();
    }

    private void control()
    {
        var yInput = Input.GetAxis("Vertical");
        var xInput = Input.GetAxis("Horizontal");

        // must do all movement on the server - send RPC
        MovementServerRpc(xInput, yInput);
    }

    [ServerRpc]
    private void MovementServerRpc(float xInput, float yInput) {
        
        var body = GetComponent<Rigidbody>();

        var transformForward = transform.forward * moveSpeed * yInput;
        
        body.AddForce(transformForward);
        body.AddTorque(Vector3.up * rotateSpeed * xInput);

        var networkTransform = GetComponent<NetworkTransform>().transform;
        
        //reset player if fallen down
        if (networkTransform.position.y < -10)
        {
            networkTransform.position = Vector3.zero;
            networkTransform.rotation = Quaternion.identity;

            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;

            points.Value = 0;
        }
    }

    [ServerRpc]
    public void ResetPointsServerRpc()
    {
        if(NetworkManager.Singleton.IsServer)
        {
            points.Value = 0;
        }
    }
}
