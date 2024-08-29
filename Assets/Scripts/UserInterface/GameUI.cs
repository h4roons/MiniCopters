using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public void Restartbtn()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenubtn()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
