using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class BubbleScore : MonoBehaviour
{
    public static BubbleScore instance;
    public TMP_Text scoreText;
    public List<TMP_Text> topScoreTexts;
    public List<TMP_Text> topScoreTexts2;

    [Header("Score Settings")]
    public float scoreSpeed = 1f;
    public float maxSpeedMultiplier = 4f;
    public float bubbleSizeFactor = 1f;
    public float minBubbleSizeForIncrease = 3f;
    public BubbleMechanic bubbleMechanic;
    public float currentScore;

    [Header("Height Limiars"), Space(6)]
    [SerializeField] private BackgroundController backgroundController;
    [Space(4)]
    [Min(0)] public float startTransitionScore;
    [Min(0)] public float endTransitionScore;

    private bool stopAll = false;
    private string filePath;
    private List<float> highScores = new List<float>();
    private const int maxRecords = 100;

    void Awake()
    {
        instance = this;
        filePath = Path.Combine(Application.persistentDataPath, "highscores.json");
        LoadScores();
    }

    void Update()
    {
        if (stopAll)
            return;

        UpdateScore();
        DisplayScore();
    }

    void UpdateScore()
    {
        if (bubbleMechanic.currentBubbleSize > minBubbleSizeForIncrease)
        {
            float scoreIncrease = bubbleMechanic.currentBubbleSize * bubbleSizeFactor * scoreSpeed;
            scoreIncrease = Mathf.Min(scoreIncrease, maxSpeedMultiplier);
            currentScore += scoreIncrease * Time.deltaTime;

            if (currentScore >= startTransitionScore && currentScore <= endTransitionScore)
            {
                backgroundController.UpdateBackground();
            }
        }
        else
        {
            currentScore -= scoreSpeed * Time.deltaTime;
            currentScore = Mathf.Max(0f, currentScore);
        }
    }

    void DisplayScore()
    {
        scoreText.text = $"{currentScore:F1}m";
    }

    public void StopScore(bool newR)
    {
        stopAll = true;
        SaveScore(currentScore, newR);
    }

    void SaveScore(float score, bool newR)
    {
        if (score <= 0)
            return;

        highScores.Add(score);
        highScores = highScores.OrderByDescending(s => s).Take(maxRecords).ToList();
        File.WriteAllText(filePath, JsonUtility.ToJson(new ScoreData { scores = highScores }));

        if(newR)
            UpdateTopScoresDisplay();
        else
            UpdateTopScoresDisplay2();
    }

    void LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);
            if (data != null && data.scores != null)
            {
                highScores = data.scores.OrderByDescending(s => s).Take(maxRecords).ToList();
            }
        }

        UpdateTopScoresDisplay();
    }

    void UpdateTopScoresDisplay()
    {
        for (int i = 0; i < topScoreTexts.Count; i++)
        {
            if (i < highScores.Count)
                topScoreTexts[i].text = $"{highScores[i]:F1}m";
            else
                topScoreTexts[i].text = "-";
        }
    }

    void UpdateTopScoresDisplay2()
    {
        for (int i = 0; i < topScoreTexts2.Count; i++)
        {
            if (i < highScores.Count)
                topScoreTexts2[i].text = $"{highScores[i]:F1}m";
            else
                topScoreTexts2[i].text = "-";
        }
    }

    [System.Serializable]
    private class ScoreData
    {
        public List<float> scores;
    }
}