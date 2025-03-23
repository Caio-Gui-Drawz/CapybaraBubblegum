using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Tags")]
    public string obstacleTag = "Obstacles";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(obstacleTag))
        {
            GameManager.Instance().OnPlayersDeath(this.transform);
            Destroy(gameObject);
        }
    }
}