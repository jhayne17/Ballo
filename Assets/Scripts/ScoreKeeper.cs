using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance;

    public TextMeshProUGUI liveScoreText;
    public TextMeshProUGUI finalScoreText;

    private int score = 0;
    private bool gameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        score = 0;
        liveScoreText.text = "Score: 0";
    }

    public void AddScore(int amount)
    {
        if (gameOver) return;
        score += amount;
        liveScoreText.text = "Score: " + score;
    }

    public void StopScore()
    {
        gameOver = true;
        finalScoreText.text = "Final Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }
}