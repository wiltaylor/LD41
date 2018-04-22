using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("game");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("tutorial");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
	
}
