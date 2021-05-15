using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject e;

    [SerializeField] GameObject blackScreen;

    [SerializeField] GameObject playerLook;

    [SerializeField] AudioClip soundClip;
    
    void Start()
    {
        EventManager.instance.onMapChanged += EChange;
    }

    // Update is called once per frame
    void EChange(bool change)
    {
        if (change)
        {
            e.SetActive(true);
        }

        if (!change)
        {
            e.SetActive(false);
        }
    }

    private void Update()
    {
        if (e.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState= CursorLockMode.None;
            Cursor.visible = true;
            
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            blackScreen.SetActive(true);
            Time.timeScale = 0f;
            playerLook.SetActive(false);
            AudioSource.PlayClipAtPoint(soundClip, transform.position, 1f);
        }
    }
}
