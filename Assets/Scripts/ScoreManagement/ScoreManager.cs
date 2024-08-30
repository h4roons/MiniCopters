using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;                         
    public GameObject levelFailedPanel;            
    public Text levelFailedScoreText;              

    private int score = 0;

    private void Awake()
    {
        scoreText.gameObject.SetActive(true);
        // Singleton pattern to ensure only one instance of ScoreManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
    }

    public void ShowLevelFailedPanel()
    {
        scoreText.gameObject.SetActive(false);
        // Set the Level Failed panel active
        levelFailedPanel.SetActive(true);

        // Update the score text on the Level Failed panel
        levelFailedScoreText.text = "Final Score: " + score.ToString();
    }
}
