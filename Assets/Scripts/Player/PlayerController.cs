using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float horizontalLimit = 8f; // Limite da tela no eixo X

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 newPosition = rb.position + Vector2.right * horizontalInput * moveSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalLimit, horizontalLimit);
        rb.MovePosition(newPosition);
    }
}