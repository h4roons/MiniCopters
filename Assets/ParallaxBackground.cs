using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float backgroundSpeed = 0.5f; 
    public float backgroundHeight;       
    public GameObject[] backgrounds;     

    void Start()
    {
        // Calculate the background height based on the first background sprite's size
        backgroundHeight = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log("Height calculated: " + backgroundHeight);
    }

    void Update()
    {
        MoveBackgrounds();
    }

    void MoveBackgrounds()
    {
        // Move each background at the defined speed downward
        foreach (GameObject bg in backgrounds)
        {
            Debug.Log (bg);
            bg.transform.Translate(Vector2.down * backgroundSpeed * Time.deltaTime);
            Debug.Log("Background moved down!");
            // Check if the background has moved off-screen to the bottom
            if (bg.transform.position.y <= -backgroundHeight)
            {
                // Reposition the background to the top for continuous scrolling
                Vector3 newPos = bg.transform.position;
                newPos.y += backgroundHeight * 2;
                bg.transform.position = newPos;
                Debug.Log("Background repositioned");
            }
        }
    }
}
