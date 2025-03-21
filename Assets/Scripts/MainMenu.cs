using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    private void Start ()
    {
        playButton.onClick.AddListener(PlayButton);
        SoundTrack.Instance().StartSoundTrack();
    }
    void PlayButton ()
    {
      SceneManager.LoadScene("GameScene");
    }
    
}