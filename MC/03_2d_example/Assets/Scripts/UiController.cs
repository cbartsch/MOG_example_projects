using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public InputField nameInput;
    public Toggle awesomeToggle;

    public void OnClickMyButton()
    {
        string name = nameInput.text;
        bool isAwesome = awesomeToggle.isOn;

        //reload the only scene that exists
        SceneManager.LoadScene(0);
    }
}
