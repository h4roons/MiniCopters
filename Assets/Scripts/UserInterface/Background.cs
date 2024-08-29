using UnityEngine;

public class Background : MonoBehaviour
{
    public float backgroundSpeed = 0.5f;
    public float backgroundHeight;
    public GameObject background;

    void Start()
    {
        // Calculate the background height based on the first background sprite's size
        backgroundHeight = background.GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log("Height calculated: " + backgroundHeight);
    }

    void Update()
    {
        MoveBackgrounds();
    }

    void MoveBackgrounds()
    {
        
            
            background.transform.Translate(Vector2.down * backgroundSpeed * Time.deltaTime);
            Debug.Log("Background moved down!");
            // Check if the background has moved off-screen to the bottom
            if (background.transform.position.y <= -backgroundHeight)
            {
                // Reposition the background to the top for continuous scrolling
                Vector3 newPos = background.transform.position;
                newPos.y += backgroundHeight * 2;
                background.transform.position = newPos;
                Debug.Log("Background repositioned");
            }
        
    }
}
