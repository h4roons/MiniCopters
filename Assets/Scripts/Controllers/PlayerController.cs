using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float liftSpeed = 3f;
    public float moveSpeed = 2f;
    public float maxY = 5f;
    public float tiltAngle = 15f;            // The maximum angle of tilt when changing direction
    public float speedIncreaseFactor = 0.1f; // The factor by which speed increases when moving
    public float tiltSmoothness = 5f;        // How smooth the tilt change is
    public float wobbleFrequency = 2f;       // Frequency of the wobble effect
    public float wobbleAmplitude = 2f;       // Amplitude of the wobble effect
    public GameObject levelFailPanel;

    private bool movingRight = true;
    private Rigidbody2D rb;

    private float minX;             // Minimum X position based on screen width
    private float maxX;             // Maximum X position based on screen width
    private float currentTilt = 0f; // Current tilt angle
    private float wobbleTime = 0f;  // Time tracker for wobble effect
    private float baseMoveSpeed;    // To store the initial move speed for reset

    void Start()
    {
        levelFailPanel.SetActive(false);
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * liftSpeed;

        // Store the initial move speed to reset it later
        baseMoveSpeed = moveSpeed;

        // Calculate the screen boundaries dynamically
        CalculateScreenBounds();
    }

    void Update()
    {
        HandleMovement();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ChangeDirection();
        }

        ClampPosition();

        // Apply smooth tilt and wobble
        ApplyTiltAndWobble();
    }

    void HandleMovement()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Gradually increase speed while moving in the current direction
        moveSpeed += speedIncreaseFactor * Time.deltaTime;
    }

    void ChangeDirection()
    {
        movingRight = !movingRight; // Change direction

        // Reset speed when changing direction
        moveSpeed = baseMoveSpeed;

        // Set target tilt based on direction
        currentTilt = movingRight ? -tiltAngle : tiltAngle;

        // Flip the copter's sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void ClampPosition()
    {
        // Restrict the copter's X position within dynamically calculated boundaries
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

    void ApplyTiltAndWobble()
    {
        // Smoothly interpolate the tilt angle
        float targetTilt = Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentTilt, Time.deltaTime * tiltSmoothness);

        // Apply a wobble effect using a sine wave
        wobbleTime += Time.deltaTime * wobbleFrequency;
        float wobble = Mathf.Sin(wobbleTime) * wobbleAmplitude;

        // Combine tilt and wobble and apply it to the rotation
        transform.rotation = Quaternion.Euler(0, 0, targetTilt + wobble);
    }

    void CalculateScreenBounds()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;

        // Calculate the screen width in world units
        float screenWidthInWorldUnits = mainCamera.orthographicSize * mainCamera.aspect;

        minX = -screenWidthInWorldUnits;
        maxX = screenWidthInWorldUnits;
    }


    void RestartLevel()
    {
        Time.timeScale = 0f;
        ScoreManager.instance.ShowLevelFailedPanel();
    }
}
