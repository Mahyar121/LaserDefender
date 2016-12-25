﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float health = 150f;
    public float firingRate = 0.5f;
    public int scoreValue = 150;
    public AudioClip fireSound;
    public AudioClip deathSound;

    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
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


    private void Update()
    {
        float prob = firingRate * Time.deltaTime;
        // prob has to be inbetween 0 - 1
        if (Random.value < prob)
        {
            EnemyFiring();
        }
    }



    private void EnemyFiring()
    {
        Vector3 startPosition = transform.position + new Vector3(0f, -1f, 0f);
        GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    private void Die()
    {
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }

}
