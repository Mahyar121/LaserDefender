using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject enemyPrefab;
    public float width = 12f;
    public float height = 7f;
    private bool movingRight = true;
    public float speed = 5f;
    public float spawnDelay = 0.5f;

    private float xmax;
    private float xmin;
    public float xpadding;
    
	// Use this for initialization
	void Start ()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distanceToCamera));
        xmax = rightBoundary.x + xpadding;
        xmin = leftBoundary.x - xpadding;
        //SpawnEnemies();
        SpawnUntilFull();
	}

    public void OnDrawGizmos()
    {
        // Draws a box around the spawner area
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    // Update is called once per frame
    void Update ()
    {
        HandleMovement();
	
	}

    void HandleMovement()
    {
        if (movingRight)
        {
            // speed * Time.deltaTime so it moves consistently on all PCs
            // transform.position += Vector3.right * speed * Time.deltaTime;
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            // transform.position += Vector3.left * speed * Time.deltaTime;
            transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
        }
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
        else if(rightEdgeOfFormation > xmax)
        {
            movingRight = !movingRight;
        }
        if (AllMembersDead())
        {
            //SpawnEnemies();
            SpawnUntilFull();
        }
    }

    bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    private Transform NextFreePosition()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition)
        {
            // Spawns an enemy, and have to declare as GameObject otherwise would be just an object
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            // Puts the spawned enemy inside the GameObject as a child
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
       
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            // Spawns an enemy, and have to declare as GameObject otherwise would be just an object
            GameObject enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity) as GameObject;
            // Puts the spawned enemy inside the GameObject as a child
            enemy.transform.parent = child;
        }
    }
}
