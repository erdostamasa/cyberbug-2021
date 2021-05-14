﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IShootable{

    int currentHealth = 100;
    [SerializeField] AudioClip deathSound;
    
    
    public int Health => currentHealth;

    void Update(){
        if (Input.GetKeyDown(KeyCode.L)){
            ReceiveProjectile(10);
        }
    }

    public void ReceiveProjectile(int damage){
        currentHealth -= damage;
        EventManager.instance.PlayerHealthChanged(currentHealth);
        if (currentHealth <= 0){
            AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.5f);
            Time.timeScale = 0;
            GetComponent<PlayerCamera>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
