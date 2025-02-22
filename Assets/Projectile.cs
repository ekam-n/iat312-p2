using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if it hit the player
        {
            Destroy(gameObject); // Destroy the projectile
        }
    }
}
