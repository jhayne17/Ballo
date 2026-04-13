using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    private int score = 0;
    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!isGameOver)
        {
            // score goes up over time
            score += (int)(Time.deltaTime * 10);
            scoreText.text = "Score: " + score;
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        finalScoreText.text = "Score: " + score;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}