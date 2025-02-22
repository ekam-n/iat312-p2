using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage = 3; // Damage value

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if we hit the player
        {
            Debug.Log("Projectile hit the player!");

            // Try to damage the player using PlayerController
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroy the projectile
        }
    }
}

