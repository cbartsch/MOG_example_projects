using UnityEngine;
using System.Collections;

public class RaycastTest : MonoBehaviour
{
    Renderer highlight;
    Color previousColor;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        var mouseRay = Camera.main.ScreenPointToRay(mousePos);
        
        RaycastHit result;
        if (Physics.Raycast(mouseRay, out result))
        {
            setHighlight(result.rigidbody);
        }
        else
        {
            setHighlight(null);
        }
    }

    private void setHighlight(Component target)
    {
        if (highlight != null)
        {
            highlight.material.color = previousColor;
            highlight = null;
        }

        if (target != null)
        {
            var renderer = target.GetComponent<Renderer>();
            highlight = renderer;
            previousColor = renderer.material.color;
            renderer.material.color = Color.green;
        }
    }
}
