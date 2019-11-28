using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DualTouch : MonoBehaviour
{
    public bool IsLeftTouch { get; private set; }
    public bool IsRightTouch { get; private set; }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //get mouse position in range [0, 1]
            var position = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            IsLeftTouch = position.x < 0.5;
            IsRightTouch = position.x >= 0.5;
        }
        else
        {
            IsLeftTouch = false;
            IsRightTouch = false;
        }
    }
}
