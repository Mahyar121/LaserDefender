using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 250f;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate;
    public float speed = 5.0f;
    public float padding = .002f;

    float xmin;
    float xmax;

	// Use this for initialization
	void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        // 0,0 is the bottom left corner
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
        // 1,0 is the bottom right corner
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandleInput();
	}

    void Fire()
    {
        // beams start above player
        Vector3 offset = new Vector3(0f, 1f, 0);
        // setting the Instantiate object as GameObject so we can set it to beam
        GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed, 0f);
    }

    void HandleInput()
    {
      
        // using the GetKey because we want to be able to hold down the button
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            // Multiplying by Time.deltaTime to have consistent movement on different pcs
            transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
        // using GetKeyDown so the player can't hold the shooting button
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If the player holds down the fire button then it will shoot lasers at a certain rate
            InvokeRepeating("Fire", 0.000001f, firingRate);
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // If the player lets go of the fire button then it will stop shooting
            CancelInvoke("Fire");
        }
        // restrict the player to the game space
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            // destroys this missile on impact
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
