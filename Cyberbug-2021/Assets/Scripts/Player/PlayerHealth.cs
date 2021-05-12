using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IShootable{

    int currentHealth = 100;
    
    public int Health => currentHealth;

    void Update(){
        if (Input.GetKeyDown(KeyCode.L)){
            ReceiveProjectile();
        }
    }

    public void ReceiveProjectile(){
        currentHealth--;
        EventManager.instance.PlayerHealthChanged(currentHealth);
    }
}
