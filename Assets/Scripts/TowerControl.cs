using UnityEngine;

public class TowerControl : MonoBehaviour
{
    public float rotationSpeed = 120f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Rotate(0f, -input * rotationSpeed * Time.deltaTime, 0f);
    }
}