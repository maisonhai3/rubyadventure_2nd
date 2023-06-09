﻿using UnityEngine;

/// <summary>
///     Handle the projectile launched by the player to fix the robots.
/// </summary>
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //destroy the projectile when it reach a distance of 1000.0f from the origin
        if (transform.position.magnitude > 1000.0f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.collider.GetComponent<Enemy>();
        if (enemy != null) 
            enemy.Fix();

        // Destroy(gameObject);
        gameObject.SetActive(false);
    }

    //called by the player controller after it instantiate a new projectile to launch it.
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
}