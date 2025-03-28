using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BubbleMenu : MonoBehaviour
{
    [Header("Bubble Settings")]
    public float maxBubbleSize = 3f;
    public float minBubbleSize = 0.1f;
    public float growthSpeed = 1f;
    public float decaySpeed = 0.5f;
    public Transform bubbleVisual;
    public GameObject popAudio;
    public GameObject playerFake;

    [Range(1f, 10f)]
    public bool isInflating = false;
    public float currentBubbleSize;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleBubbleInput();
        UpdateBubbleVisual();
        CheckForExplosion();
    }

    private void HandleBubbleInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentBubbleSize += growthSpeed * Time.deltaTime;
            isInflating = true;
        }
        else
        {
            currentBubbleSize -= decaySpeed * Time.deltaTime;
            isInflating = false;
        }

        currentBubbleSize = Mathf.Clamp(currentBubbleSize, minBubbleSize, maxBubbleSize);
    }

    private void UpdateBubbleVisual()
    {
        if (bubbleVisual != null)
            bubbleVisual.localScale = Vector3.one * currentBubbleSize;
    }

     private void CheckForExplosion()
    {
        if (currentBubbleSize >= maxBubbleSize)
        {
            popAudio.SetActive(true);
            StartCoroutine(ChangeSceneAfterDelay());
            Destroy(playerFake);
        }
    }

    private IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameScene");
    }
}