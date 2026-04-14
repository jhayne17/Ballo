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
            Debug.Log("Game Over");
            HighScore.TRY_SET_HIGH_SCORE(ScoreCounter.Instance.score);
            GameManager.Instance.GameOver();

        }

        if (collision.gameObject.CompareTag("Safe"))
    {
        ScoreCounter.Instance.score++;
    }
    }
}