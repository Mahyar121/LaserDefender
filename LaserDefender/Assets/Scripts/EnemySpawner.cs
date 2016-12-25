using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;

	// Use this for initialization
	void Start ()
    {
       // for every child in parent spawn an enemy at location
        foreach(Transform child in transform)
        {
            // Spawns an enemy at (0,0,0) and have to declare as GameObject otherwise would be just an object
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
            // Puts the spawned enemy inside the GameObject as a child
            enemy.transform.parent = childd;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
