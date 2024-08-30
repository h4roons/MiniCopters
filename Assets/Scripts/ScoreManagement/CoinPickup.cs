using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;                      // Value of each coin
    public AudioClip pickupSound;                  // Sound to play when a coin is picked up
    public GameObject coinPrefab;                  // Reference to the coin prefab
    public float respawnTime = 2f;                 // Time before the coin respawns
                 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))        // Check if the colliding object is the player
        {
            // Update the player's score
            ScoreManager.instance.AddScore(coinValue);

            // Play the pickup sound
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // Start the respawn coroutine
            StartCoroutine(RespawnCoin());
        }
    }

    private IEnumerator RespawnCoin()
    {
        // Disable the coin before destruction
        gameObject.GetComponent<SpriteRenderer>().enabled =false;

        // Wait for the respawn time
        yield return new WaitForSeconds(respawnTime);

        // Re-enable the coin, reset its position, and reassign its parent
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //transform.position = GetRandomSpawnPosition(); // Optional: Set a new position if needed
        //transform.SetParent(parentObject); // Reassign the parent of the coin
    }

}
