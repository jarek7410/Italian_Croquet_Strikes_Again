using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("TrainLevel");
    }

    public void ExitGame()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
