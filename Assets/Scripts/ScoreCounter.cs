using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using TMPro;           

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;  

    [Header("Dynamic")]
    public int score = 0;
    private TextMeshProUGUI uiText;  

    void Awake()
    {
        Instance = this;  
    }

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        uiText.text = "Your current score: " + score.ToString("#0");  
    }
}