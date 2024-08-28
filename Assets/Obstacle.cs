using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;         

public class Obstacle : MonoBehaviour
{
    public GameObject playerController;   
    public GameObject parallaxBackground; 
    public float collisionGravity = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.gravityScale = collisionGravity; 

           
            playerController.GetComponent<PlayerController>().enabled = false;
            parallaxBackground.GetComponent<ParallaxBackground>().enabled = false;

            
            StartCoroutine(RestartLevelAfterDelay(2f)); 
        }
    }

    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 

     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
