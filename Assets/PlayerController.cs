using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public float GlideForce = 2f;
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

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            float jumpPower = HasUmbrella ? JumpForce * 1.5f : JumpForce; // Higher jump with umbrella
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }

        // Gliding
        if (HasUmbrella && Input.GetButton("Jump") && !isGrounded && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -GlideForce); // Slow descent
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
