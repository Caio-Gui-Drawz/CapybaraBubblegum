using UnityEngine;

public class BubbleMechanic : MonoBehaviour
{
    [Header("Bubble Settings")]
    public float maxBubbleSize = 3f;
    public float minBubbleSize = 0.1f;
    public float growthSpeed = 1f;
    public float decaySpeed = 0.5f;
    public Transform bubbleVisual;
    public CircleCollider2D circleCollider;

    [Header("Movement")]
    public float moveSpeed = 5f; // Velocidade horizontal
    public float horizontalLimit = 8f; // Limite da tela

    [Header("Propulsion")]
    public float propulsionForce = 10f;
    [Range(1f, 10f)]
    public float gravoffset;
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
        HandleMovement(); // Adicionado controle de movimento
        UpdateBubbleVisual();
        CheckForExplosion();
        SetGravitySpeed();
        SetColliderRadius();
    }

    // Novo método para movimento horizontal
    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // Usar GetAxisRaw para resposta instantânea
        Vector2 velocity = rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;
        rb.linearVelocity = velocity;

        // Limitar posição horizontal
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -horizontalLimit, horizontalLimit);
        transform.position = clampedPosition;
    }

    private void SetGravitySpeed(){
        GameManager.Instance().SetSpeed(-((currentBubbleSize * 20f) - gravoffset));
    }

    private void SetColliderRadius()
    {
        float radius = Mathf.Clamp((currentBubbleSize * 4f), 0.4f, 3f);
        circleCollider.radius = radius;
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
        if (currentBubbleSize >= maxBubbleSize || currentBubbleSize <= minBubbleSize)
        {
            GameManager.Instance().OnPlayersDeath(this.transform);
            Destroy(gameObject);
        }
    }

    public void ApplyPropulsion()
    {
        if (isInflating)
        {
            rb.AddForce(Vector2.up * propulsionForce, ForceMode2D.Impulse);
            currentBubbleSize = minBubbleSize; // Reset
        }
    }
}