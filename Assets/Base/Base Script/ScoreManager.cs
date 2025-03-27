using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("Health UI")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image healthBar;

    [Header("Score UI")]
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
        if (playerHealth != null)
            playerHealth.OnHealthChanged += UpdateHealthUI;

        UpdateScoreUI();
        UpdateHealthUI(playerHealth.Health, playerHealth.MaxHealth);
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdateHealthUI;
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        healthText.text = $"{currentHealth}/{maxHealth}";
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"{score}/{maxScore}";
    }

    public void SetMaxScore(int value)
    {
        maxScore = value;
        UpdateScoreUI();
        Debug.Log("Update MaxScore : " + maxScore);
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();
    }
}