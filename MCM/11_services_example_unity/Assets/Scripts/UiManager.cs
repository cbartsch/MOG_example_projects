using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    public Text rewardedText;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        rewardedText.gameObject.SetActive(AdManager.isRewarded);
    }
}