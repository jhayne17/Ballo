using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        vel.x = 0f;
        vel.z = 0f;
        rb.velocity = vel;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DangerPlatform"))
        {
            isDead = true;
            // GameManager.Instance.GameOver(); // comment this out for now
        }
        if (collision.gameObject.CompareTag("SafePlatform"))
        {
            isDead = false;
        }
    }
}