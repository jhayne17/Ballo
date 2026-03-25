using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool isDead = false;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        vel.x = 0f;
        vel.z = 0f;
        rb.velocity = vel;
    }
   void OnCollision(Collision collision)
    {
       if(collision.GameObject.compareTag("DangerPlatform"))
        {
            isDead = true;
            GameManager.Instance.GameOver();
        }
        if (collision.GameObject.compareTag("SafePlatform"))
        {
            isDead = false;
        }
    }
}
