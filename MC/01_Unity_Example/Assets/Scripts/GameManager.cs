using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject entityPrefab;

    private void Start()
    {
        //call the method every second, starting after 5 seconds
    //    InvokeRepeating("SpawnPrefab", 5, 1);

        //call our custom co routine
        //do not just call the coroutine method - use StartCoroutine!
        StartCoroutine(myCoroutine());
    }

    private IEnumerator myCoroutine()
    {
        Debug.Log("waiting 3 seconds");
        yield return new WaitForSeconds(3);
        SpawnPrefab();

        Debug.Log("waiting 2 seconds");
        yield return new WaitForSeconds(2);
        SpawnPrefab();

        while (true)
        {
            float time = Random.Range(1f, 5f);
            Debug.Log("spawning in " + time + " seconds");
            yield return new WaitForSeconds(time);
            SpawnPrefab();
        }
    }

    void Update ()
    {
        //spawn an object on click
        if (Input.GetMouseButtonDown(0)) {
            SpawnPrefab();
        }
	}

    private void SpawnPrefab()
    { 
        float maxPos = 5f;
        var position = new Vector3(
            Random.Range(-maxPos, maxPos),  //x
            Random.Range(-maxPos, maxPos),  //y
            Random.Range(-maxPos, maxPos)); //z

        GameObject created = GameObject.Instantiate(entityPrefab, position, Quaternion.identity);

        Debug.Log("spawned an object", created);
    }



}


