using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonShooter : MonoBehaviour
{

    public GameObject bulletPrefab;

    public float shootForce = 100;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootToCenter();
            //ShootToMousePosition();
            //ShootAngled();
        }
    }

    private void ShootToCenter()
    {
        //instantiate bullet at camera position
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        //apply impulse in direction to mouse position
        var body = bullet.GetComponent<Rigidbody>();
        body.AddForce(transform.forward * shootForce, ForceMode.Impulse);
    }

    private void ShootToMousePosition()
    {
        //get ray to mouse position
        var dirRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //instantiate bullet at camera position
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        //apply impulse in direction to mouse position
        var body = bullet.GetComponent<Rigidbody>();
        body.AddForce(dirRay.direction * shootForce, ForceMode.Impulse);
    }

    private void ShootAngled()
    {
        //instantiate bullet in front of camera position
        var bullet = Instantiate(bulletPrefab, transform.position + transform.forward * 1, 
            Quaternion.identity);

        //get mouse position in interval [0, 1]
        var mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //shoot at x-angle in interval [-60, 60] and y-angle in interval [0, -60]
        var maxAngle = 60;
        var xAngle = (mousePos.x * 2 - 1) * maxAngle;
        var yAngle = -mousePos.y * maxAngle;

        //calculate x-rotation using up-axis and y-rotation using right-axis
        var rotation = Quaternion.AngleAxis(xAngle, transform.up) *
            Quaternion.AngleAxis(yAngle, transform.right);

        //apply force to rotated forward vector (multiplication = apply quaternion)
        var body = bullet.GetComponent<Rigidbody>();
        body.AddForce(rotation * transform.forward * shootForce, ForceMode.Impulse);
    }
}
