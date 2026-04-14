using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    static private TextMeshProUGUI _UI_TEXT;
    static private int _SCORE = 0;

    [Tooltip("Check this box to reset the HighScore in PlayerPrefs")]
    public bool resetHighScoreNow = false;

    void Awake()
    {
        _UI_TEXT = GetComponent<TextMeshProUGUI>();

        // if a highscore already exists in PlayerPrefs, read it
        if (PlayerPrefs.HasKey("HighScore"))
        {
            SCORE = PlayerPrefs.GetInt("HighScore");
        }

        // assign the high score to PlayerPrefs
        PlayerPrefs.SetInt("HighScore", _SCORE);
    }

    static public int SCORE
    {
        get { return _SCORE; }
        private set
        {
            _SCORE = value;
            PlayerPrefs.SetInt("HighScore", value);
            if (_UI_TEXT != null)
            {
                _UI_TEXT.text = "High Score: " + value.ToString("#,0");
            }
        }
    }

    static public void TRY_SET_HIGH_SCORE(int scoreToTry)
    {
        if (scoreToTry <= SCORE) return;
        SCORE = scoreToTry;
    }

    // lets you reset the high score from the Inspector
    void OnDrawGizmos()
    {
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;
            PlayerPrefs.SetInt("HighScore", 0);
            Debug.LogWarning("PlayerPrefs HighScore reset to 0.");
        }
    }
}