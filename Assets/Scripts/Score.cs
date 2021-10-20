using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text textScore;
    public int AmountScore { get; private set; }

    private void Awake()
    {
        textScore = GetComponent<Text>();
        AmountScore = 0;
        textScore.text = $"{AmountScore}";
    }

    public void AddScore(int amount) 
    {
        AmountScore += amount;
        Debug.Log($"Добавлено {amount} очков");
        textScore.text = $"{AmountScore}";
    }
    public void SubtractScore(int amount) 
    {
        AmountScore -= amount;
        AmountScore = Mathf.Clamp(AmountScore,0,1000);
        textScore.text = $"{AmountScore}";
    }
}
