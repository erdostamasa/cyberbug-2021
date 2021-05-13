using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressE : MonoBehaviour
{
    [SerializeField] GameObject e;
    // Start is called before the first frame update
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
