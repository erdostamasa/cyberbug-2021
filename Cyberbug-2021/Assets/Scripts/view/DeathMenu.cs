using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour{

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject pauseMenu;
    
    void Awake(){
        EventManager.instance.onPlayerHealthChanged += Appear;
    }

    void Appear(int health){
        if (health <= 0){
            pauseMenu.SetActive(false);
            canvas.SetActive(true);
            Cursor.lockState= CursorLockMode.None;
            Cursor.visible = true;
            weaponHolder.SetActive(false);
        }
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}