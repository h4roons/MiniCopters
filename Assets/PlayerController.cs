using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float liftSpeed = 3f;      
    public float moveSpeed = 2f;     
    public float minX = -2.5f;        
    public float maxX = 2.5f;         
    public float maxY = 5f;           
    private bool movingRight = true;  

    private Rigidbody2D rb;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * liftSpeed; 
    }

    void Update()
    {
        HandleMovement();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ChangeDirection();
        }

        ClampPosition();
    }

    void HandleMovement()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    void ChangeDirection()
    {
        movingRight = !movingRight; // Changing direction from right to left
        Debug.Log(movingRight);
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; 
        transform.localScale = localScale;
    }

    void ClampPosition()
    {
        // Restricting the helicopter's X axis position so that it doesn't gets out of the bounds / screen
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, minX, maxX);

        // Restrict the copter's Y position to maxY
        clampedPosition.y = Mathf.Clamp(transform.position.y, -Mathf.Infinity, maxY);

        transform.position = clampedPosition;

        // Restart the level if the copter hits the screen edges
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
