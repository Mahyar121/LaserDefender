using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject enemyPrefab;
    public float width = 12f;
    public float height = 7f;
    public float speed = 5f;
    public float spawnDelay = 0.5f;

    private int direction = 1;
    private float boundaryRightEdge, boundaryLeftEdge;
    public float xpadding = 1;
    
	// Use this for initialization
	void Start ()
    {
        Camera camera = Camera.main;
        float distanceToCamera = transform.position.z - camera.transform.position.z;
        boundaryLeftEdge = camera.ViewportToWorldPoint(new Vector3(0f, 0f, distanceToCamera)).x + xpadding;
        boundaryRightEdge = camera.ViewportToWorldPoint(new Vector3(1f, 1f, distanceToCamera)).x - xpadding;
        SpawnUntilFull();
	}

    public void OnDrawGizmos()
    {
        float xmin, xmax, ymin, ymax;
        xmin = transform.position.x - 0.5f * width;
        xmax = transform.position.x + 0.5f * width;
        ymin = transform.position.y - 0.5f * height;
        ymax = transform.position.y + 0.5f * height;
        Gizmos.DrawLine(new Vector3(xmin, ymin, 0), new Vector3(xmin, ymax));
        Gizmos.DrawLine(new Vector3(xmin, ymax, 0), new Vector3(xmax, ymax));
        Gizmos.DrawLine(new Vector3(xmax, ymax, 0), new Vector3(xmax, ymin));
        Gizmos.DrawLine(new Vector3(xmax, ymin, 0), new Vector3(xmin, ymin));
    }
    // Update is called once per frame
    void Update ()
    {
        HandleMovement();
	
	}

    void HandleMovement()
    {
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < boundaryLeftEdge)
        {
            direction = 1;
        }
        else if(rightEdgeOfFormation > boundaryRightEdge)
        {
            direction = -1;
        }
        if (AllMembersDead())
        {
            SpawnUntilFull();
        }
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);
        if(AllMembersDead())
        {
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

}
