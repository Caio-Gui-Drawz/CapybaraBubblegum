using UnityEngine;

public class MoveParent : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Movimento básico para o objeto pai
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime);
    }
}
