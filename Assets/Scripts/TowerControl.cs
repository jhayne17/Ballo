using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    public float rotationSpeed = 120f;
    public float mouseSpeed = 5f;

    private float lastMouseX;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Rotate(0f, -input * rotationSpeed * Time.deltaTime, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            lastMouseX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            float delta = Input.mousePosition.x - lastMouseX;
            transform.Rotate(0f, -delta * mouseSpeed * Time.deltaTime, 0f);
            lastMouseX = Input.mousePosition.x;
        }
    }
}