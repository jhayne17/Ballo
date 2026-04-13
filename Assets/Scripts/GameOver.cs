using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kill"))
        {
            TriggerGameOver();
        }

        if (collision.gameObject.CompareTag("Safe"))
        {
            ScoreKeeper.Instance.AddScore(1);
            Debug.Log("Safe platform hit");
        }
    }

    void TriggerGameOver()
{
    gameOverPanel.SetActive(true);

    ScoreKeeper.Instance.StopScore();

    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
}
}