using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject allEndScreen, newRecord;
    public TMP_Text scoreText;
    public float speedmult;
    public float vertical_speed{get;private set;}

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

    public void GameOver()
    {
        Time.timeScale = 0; // Pausa o jogo
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
        SoundTrack.Instance().StopSoundTrack();
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