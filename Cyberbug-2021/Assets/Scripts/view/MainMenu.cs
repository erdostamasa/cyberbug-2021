using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void StartLevel1()
    {
        SceneManager.LoadScene(1);
    }
    
    public void StartLevel2()
    {
        SceneManager.LoadScene(2);
    }
    
    public void StartLevel3()
    {
        SceneManager.LoadScene(3);
    }
    
    public void StartArena()
    {
        SceneManager.LoadScene(4);
    }

}
