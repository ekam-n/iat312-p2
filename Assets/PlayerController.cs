using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    public float MoveSpeed = 5f;
    public float JumpForce = 6f;  // Standard jump force, no increase with umbrella
    public float GlideGravityScale = 0.1f;  // Lower gravity scale to slow the fall while gliding
    public float JumpBoostForce = 8f;  // Vertical boost force when right-clicking
    public bool HasUmbrella = false;

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
            // Jump force stays the same whether umbrella is picked up or not
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        }

        // Gliding (Hold Left-Click when not grounded)
        if (HasUmbrella && Input.GetMouseButton(0) && !isGrounded && rb.linearVelocity.y < 0)
        {
            rb.gravityScale = GlideGravityScale;  // Set low gravity for slow fall
        }
        else
        {
            // Reset gravity scale when not gliding
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

    private bool CheckGrounded()
    {
        // Ground check using a raycast
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
        // Check if the player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.gravityScale = 2.5f; // Reset gravity scale when on the ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player is no longer grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
