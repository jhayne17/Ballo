using UnityEngine;

public class BallBounceLogic : MonoBehaviour
{
    public float bounceForce = 8f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * bounceForce, ForceMode.VelocityChange);
    }
}