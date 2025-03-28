using UnityEngine;

public class BubbleTremorMenu : MonoBehaviour
{
    public BubbleMenu bubble;
    private Vector3 originalPosition;
    private bool isTrembling = false;

    [Header("Tremor Settings")]
    public float tremorMagnitude = 0.05f;
    public float tremorSpeed = 0.1f;
    public float tremorThreshold = 0.5f;

    private float nextShakeTime = 0f;
    private Transform parentTransform;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        originalPosition = transform.localPosition;
        parentTransform = transform.parent;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (bubble.currentBubbleSize >= tremorThreshold)
        {
            if (!isTrembling)
            {
                isTrembling = true;
            }

            if (Time.time >= nextShakeTime)
            {
                nextShakeTime = Time.time + tremorSpeed;

                float shakeX = Random.Range(-tremorMagnitude, tremorMagnitude);
                float shakeY = Random.Range(-tremorMagnitude, tremorMagnitude);

                transform.localPosition = originalPosition + new Vector3(shakeX, shakeY, 0);

                float speedFactor = Mathf.InverseLerp(tremorThreshold, 0.7f, bubble.currentBubbleSize);
                tremorSpeed = Mathf.Lerp(0.1f, 0.01f, speedFactor);

                if (spriteRenderer != null)
                {
                    float alpha = Mathf.Lerp(255f, 200f, speedFactor);
                    Color color = spriteRenderer.color;
                    color.a = alpha / 255f;
                    spriteRenderer.color = color;
                }
            }
        }
        else
        {
            if (isTrembling)
            {
                isTrembling = false;
                transform.localPosition = originalPosition;

                if (spriteRenderer != null)
                {
                    Color color = spriteRenderer.color;
                    color.a = 1f;
                    spriteRenderer.color = color;
                }
            }
        }
    }
}
