using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    public float rotationSpeed = 100f;

    void Update()
    {
        float input = 0f;
        //this gets the players a and d keys, a moves left, d moves right
        if (Input.GetKey(KeyCode.A))
            input = 1f;
        if (Input.GetKey(KeyCode.D))
            input = -1f;

        transform.Rotate(0f, input * rotationSpeed * Time.deltaTime, 0f);
   }

}
