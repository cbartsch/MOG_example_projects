using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSwitcher : MonoBehaviour
{ 
    public void ChangeInput(Dropdown changed)
    {
        //cast dropdown index to InputMode enum
        var mode = (CustomInputManager.InputMode)changed.value;
        CustomInputManager.SetInputMode(mode);
    }
}
