using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTower : MonoBehaviour
{
    public float movementSpeed = 2f;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, movementSpeed * Time.deltaTime, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, -movementSpeed * Time.deltaTime, 0f);
        }
    }
}