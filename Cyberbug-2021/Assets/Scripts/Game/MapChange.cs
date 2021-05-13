using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChange : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        EventManager.instance.MapChanged(true);
    }

    void OnTriggerExit(Collider other)
    {
        EventManager.instance.MapChanged(false);
    }
}
