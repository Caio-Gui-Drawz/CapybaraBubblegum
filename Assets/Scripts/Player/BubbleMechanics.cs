using UnityEngine;

public class BubbleMechanic : MonoBehaviour
{
    [Header("Bubble Settings")]
    public float maxBubbleSize = 3f;
    public float minBubbleSize = 0.1f;
    public float growthSpeed = 1f;
    public float decaySpeed = 0.5f;
    public Transform bubbleVisual;
    


    [Header("Movement")]
    public float moveSpeed = 5f; // Velocidade horizontal
    public float horizontalLimit = 8f; // Limite da tela

    [Header("Propulsion")]
    public float propulsionForce = 10f;
    [Range(1f, 10f)]
    public float gravoffset;

    private float currentBubbleSize;
    private bool isInflating;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentBubbleSize = transform.localScale.x;

    }

    void Update()
    {
        HandleBubbleInput();
        HandleMovement(); // Adicionado controle de movimento
        UpdateBubbleVisual();
        CheckForExplosion();
        SetGravitySpeed();
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
    GameManager.Instance().SetSpeed(-(currentBubbleSize - gravoffset));
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
              Destroy(gameObject);
            GameManager.Instance().OnPlayersDeath();
            
           // Exemplo simples
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