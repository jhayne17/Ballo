using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour
{
    public int scoreThreshold = 10;
    private int lastRotationScore = 0;

    void Update()
    {
        if (ScoreCounter.Instance.score >= lastRotationScore + scoreThreshold)
        {
            lastRotationScore = ScoreCounter.Instance.score;
            float randomZ = Random.Range(0f, 360f);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, randomZ);
        }
    }
}