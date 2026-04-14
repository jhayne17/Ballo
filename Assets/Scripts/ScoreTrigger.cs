using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool scored = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !scored)
        {
            scored = true;
            ScoreCounter.Instance.score++;
        }
    }
}