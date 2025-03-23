using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public GameObject allEndScreen, newRecord;
    public TMP_Text scoreText;
    public float speedmult;
    public float vertical_speed{get;private set;}
    public Spawn spawnManager;
    public bool inGame = false;
    public ParticleSystem deathParticleSystem;
    
    private float nextThreshold = 50f;
    private const float increaseAmount = 0.05f;
    private const float maxChanceTwo = 0.6f;
    private const float maxChanceThree = 0.4f;

    public void SetSpeed(float newspeed){
        vertical_speed = newspeed * speedmult;
    }

    public static GameManager Instance() => instance;
    private static GameManager instance;

    [Header("UI")]
    public GameObject gameOverPanel;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if(!inGame)
            return;

        if (BubbleScore.instance.currentScore >= nextThreshold)
        {
            if (spawnManager != null)
            {
                spawnManager.chanceTwoObjects = Mathf.Min(spawnManager.chanceTwoObjects + increaseAmount, maxChanceTwo);
                spawnManager.chanceThreeObjects = Mathf.Min(spawnManager.chanceThreeObjects + increaseAmount, maxChanceThree);
                spawnManager.secondSpawn = Mathf.Max(spawnManager.secondSpawn - 0.1f, 0.8f);
            }

            nextThreshold += 50f;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPlayersDeath()
    {
        BubbleScore.instance.StopScore();
        vertical_speed = 0;
        speedmult = 0;
        SoundTrack.Instance().StopSoundTrack();

        if(deathParticleSystem != null)
            deathParticleSystem.Play();

        ScreenShake.Instance.TriggerShake();

        StartCoroutine(DelayDeath());
    }

    private IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(2);
        allEndScreen.SetActive(true);
        scoreText.text = $"{BubbleScore.instance.currentScore:F1}m";

        float savedScore = PlayerPrefs.GetFloat("Score", 0f);

        if(savedScore < BubbleScore.instance.currentScore) {
            PlayerPrefs.SetFloat("Score", BubbleScore.instance.currentScore);
            newRecord.SetActive(true);
        }
    }
}
