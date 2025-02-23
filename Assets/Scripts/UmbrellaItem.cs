using UnityEngine;

public class UmbrellaItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Give the player the umbrella ability
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.PickUpUmbrella();
                Destroy(gameObject); // Destroy the umbrella after pickup
            }
        }
    }
}