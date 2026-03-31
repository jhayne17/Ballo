using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kill"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Safe"))
        {
            Debug.Log("Safe platform hit");
        }
    }
}