using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kill"))
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}