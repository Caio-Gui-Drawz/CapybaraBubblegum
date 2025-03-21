using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    void Update()
    {
        // Verifica se a tecla "R" foi pressionada
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reinicia a cena atual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
