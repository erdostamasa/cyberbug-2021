using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitZone : MonoBehaviour{
    float damageCooldown = 1f;
    float timer = 0;
    bool canDamage = true;

    void Update(){
        timer += Time.deltaTime;
        if (timer >= damageCooldown){
            canDamage = true;
            timer = 0;
        }
    }

    void OnTriggerStay(Collider other){
        if (other.CompareTag("Player") && canDamage){
            Debug.Log("DAMAGED!");
            canDamage = false;
        }
    }
}