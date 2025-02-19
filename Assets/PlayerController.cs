using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public float GlideForce = 2f;
    public float BoostForce = 7f; // Jump boost force when right-clicking
    public bool HasUmbrella = false;

    // References
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumpingOrGliding; // Track if the player is in the air (jumping or gliding)

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

        // Normal Jump (Spacebar)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Jump Boost (Right-Click) - Only if umbrella is collected and player is jumping or gliding
        if (HasUmbrella && Input.GetMouseButtonDown(1) && isJumpingOrGliding)
        {
            JumpBoost();
        }

        // Gliding (Hold Spacebar)
        if (HasUmbrella && Input.GetButton("Jump") && !isGrounded && rb.linearVelocity.y < 0)
        {
            isJumpingOrGliding = true; // Player is gliding

            // Mouse control for glide direction
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float glideDirection = mousePosition.x - transform.position.x; // Get the horizontal difference

            // Apply glide force in the direction of the mouse
            rb.linearVelocity = new Vector2(glideDirection * GlideForce, rb.linearVelocity.y); // Glide horizontally towards the mouse
        }
        else if (isGrounded || rb.linearVelocity.y >= 0)
        {
            isJumpingOrGliding = false; // Player is on the ground or ascending
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        isJumpingOrGliding = true; // Player is jumping
    }

    private void JumpBoost()
    {
        Debug.Log("Jump Boost Activated!");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, BoostForce);
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
            isJumpingOrGliding = false; // Reset when hitting the ground
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
