using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int maxScore;

    private int score;

    private void Start()
    {
        score = 0;
        maxScore = 0;
        UpdateUI();
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + score + " / " + maxScore;
    }

    public void SetMaxScore(int value)
    {
        maxScore = value;
        UpdateUI();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
    }
}