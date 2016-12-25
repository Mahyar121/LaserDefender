using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;

	// Use this for initialization
	void Start ()
    {
        Instantiate(enemyPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
