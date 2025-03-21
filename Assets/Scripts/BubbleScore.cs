using UnityEngine;
using TMPro;

public class BubbleScore : MonoBehaviour
{
    public static BubbleScore instance;
    public TMP_Text scoreText;
    [Header("Score Settings")]
    public float scoreSpeed = 1f;
    public float maxSpeedMultiplier = 2f;
    public float bubbleSizeFactor = 1f;
    public float minBubbleSizeForIncrease = 3f;
    public BubbleMechanic bubbleMechanic;
    public float currentScore;

    private bool stopAll = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        UpdateScore();
        DisplayScore();
    }

    void UpdateScore()
    {
        if(stopAll)
            return;

        if (bubbleMechanic.currentBubbleSize > minBubbleSizeForIncrease)
        {
            float scoreIncrease = bubbleMechanic.currentBubbleSize * bubbleSizeFactor * scoreSpeed;
            scoreIncrease = Mathf.Min(scoreIncrease, maxSpeedMultiplier);
            currentScore += scoreIncrease * Time.deltaTime;
        }
        else
        {
            currentScore -= scoreSpeed * Time.deltaTime;
            currentScore = Mathf.Max(0f, currentScore); // Garantir que a pontuação não vá abaixo de 0
        }
    }

    void DisplayScore()
    {
        scoreText.text = $"{currentScore:F1}m";
    }

    public void StopScore() {
        stopAll = true;
    }
}
