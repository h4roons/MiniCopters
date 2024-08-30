using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Cinemachine;

public class Obstacle : MonoBehaviour
{
    public GameObject playerController;
    public GameObject bg1;
    public GameObject bg2;
    public float collisionGravity = 0.2f;

    public GameObject destructionParticles;
    public GameObject levelFailPanel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.gravityScale = collisionGravity;

            // Disable player controller and background scripts
            playerController.GetComponent<PlayerController>().enabled = false;
            bg1.GetComponent<Background>().enabled = false;
            bg2.GetComponent<Background>().enabled = false;

            // Trigger the Destruction Particles
            if (destructionParticles != null)
            {
                destructionParticles.SetActive(true);
            }

            StartCoroutine(RestartLevelAfterDelay(2f));
        }
    }

    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ScoreManager.instance.ShowLevelFailedPanel();
        destructionParticles.SetActive(false);
    }
}
