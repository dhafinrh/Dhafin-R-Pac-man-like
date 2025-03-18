using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int maxScore;

    private int score;

    private void Awake()
    {
        score = 0;
        maxScore = 0;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + score + "/" + maxScore;
    }

    public void SetMaxScore(int value)
    {
        maxScore = value;
        UpdateUI();
        Debug.Log("Update MaxScore : " + maxScore);
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
    }
}