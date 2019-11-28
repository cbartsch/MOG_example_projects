using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragGameObject : MonoBehaviour {

    GameObject selected;
    Vector3 lastMousePos;

	void Update ()
    {
        var mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            //use physics to find object under mouse
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit) &&
                hit.collider.gameObject != null)
            {
                selected = hit.collider.gameObject;
            }
        }
        if (Input.GetMouseButton(0) && selected != null)
        {
            //calculate mouse movement
            var dp = mousePos - lastMousePos;
            //move X and Z axis of object with mouse
            var movement = new Vector3(dp.x, 0, dp.y) / Screen.dpi;
            selected.transform.position += movement;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //stop dragging when mouse is released
            selected = null;
        }
        lastMousePos = mousePos;
    }
}
