using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour{
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHealth>().ReceiveProjectile(9999);
        }
    }
}