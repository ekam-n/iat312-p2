using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 3f; // Movement speed
    public float moveDistance = 5f; // Distance to move before turning
    private Vector3 startPosition;
    private int direction = 1; // 1 = right, -1 = left

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveDistance)
        {
            direction *= -1; // Reverse direction
        }
    }

    // Kill the player on collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if it's the player
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die(); // Call the player's death function
            }
        }
    }
}
