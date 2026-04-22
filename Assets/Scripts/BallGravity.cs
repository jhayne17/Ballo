using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGravity : MonoBehaviour
{
    public float initialGravity = 9.81f;
    public float gravityIncrement = 2f;
    public int scoreThreshold = 15;

    private Rigidbody rb;
    private int lastSpeedScore = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -initialGravity, 0f);
    }

    void Update()
    {
        if (ScoreCounter.Instance.score >= lastSpeedScore + scoreThreshold)
        {
            lastSpeedScore = ScoreCounter.Instance.score;
            initialGravity += gravityIncrement;
            Physics.gravity = new Vector3(0f, -initialGravity, 0f);
        }
    }
}