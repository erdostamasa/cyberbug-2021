using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChange : MonoBehaviour{
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")){
            EventManager.instance.MapChanged(true);
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.CompareTag("Player")){
            EventManager.instance.MapChanged(false);
        }
    }
}