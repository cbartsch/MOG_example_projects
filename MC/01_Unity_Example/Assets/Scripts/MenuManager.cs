using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public string gameSceneName;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
    void Update() {
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
