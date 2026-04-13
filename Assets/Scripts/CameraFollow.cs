using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ball;
    public float yOffset = 0f;
    public float followSpeed = 6f;

    private float fixedX;
    private float fixedZ;

    void Start()
    {
        fixedX = transform.position.x;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (ball == null) return;

        Vector3 targetPosition = new Vector3(
            fixedX,
            ball.position.y + yOffset,
            fixedZ
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }
}