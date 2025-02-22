using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject projectilePrefab;  // Assign the projectile prefab in the Inspector
    public float shootInterval = 2f;     // Time between shots
    public float projectileSpeed = 5f;   // Speed of the projectile

    void Start()
    {
        // Repeatedly call the Shoot function
        InvokeRepeating("Shoot", 1f, shootInterval);
    }

    void Shoot()
    {
        // Create the projectile at the enemy's position
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        
        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Set the projectile to move upwards
        rb.linearVelocity = new Vector2(0, projectileSpeed);

        // Destroy the projectile after a few seconds to save memory
        Destroy(projectile, 5f);
    }
}

