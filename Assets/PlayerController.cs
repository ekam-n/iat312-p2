using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    public float MoveSpeed = 5f;
    public float JumpForce = 6f;  // Standard jump force, no increase with umbrella
    public float GlideGravityScale = 0.1f;  // Lower gravity scale to slow the fall while gliding
    public float JumpBoostForce = 8f;  // Vertical boost force when right-clicking
    public bool HasUmbrella = false;

    // Health
    public int health = 1; // Player starts with 3 health

    // References
    private Rigidbody2D rb;
    private bool isGrounded;

    // Umbrella Visual
    public GameObject UmbrellaSprite; // Assign the umbrella sprite in the Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (UmbrellaSprite != null)
        {
            UmbrellaSprite.SetActive(false); // Hide the umbrella initially
        }
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * MoveSpeed, rb.linearVelocity.y);

        // Jumping (W key)
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        }

        // Gliding (Hold Left-Click when not grounded)
        if (HasUmbrella && Input.GetMouseButton(0) && !isGrounded && rb.linearVelocity.y < 0)
        {
            rb.gravityScale = GlideGravityScale;  // Set low gravity for slow fall
        }
        else
        {
            if (!isGrounded)
            {
                rb.gravityScale = 2.5f;  // Reset gravity to normal when not gliding
            }
        }

        // Jump Boost while gliding (Right-click)
        if (HasUmbrella && !isGrounded && Input.GetMouseButtonDown(1)) // Right-click for boost
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpBoostForce); // Apply vertical boost
        }
    }

    // Make TakeDamage public so other scripts can access it
    public void TakeDamage(int damage)
    {
        health -= damage + 3;
        Debug.Log("Player took damage! Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        Destroy(gameObject); // Remove player from the scene
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) // If hit by a projectile
        {
            TakeDamage(3);
            Destroy(other.gameObject); // Destroy the projectile
        }
    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null && hit.collider.CompareTag("Ground");
    }

    public void PickUpUmbrella()
    {
        HasUmbrella = true;
        Debug.Log("Umbrella picked up!");
        if (UmbrellaSprite != null)
        {
            UmbrellaSprite.SetActive(true); // Show the umbrella on the player
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.gravityScale = 2.5f; // Reset gravity scale when on the ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
