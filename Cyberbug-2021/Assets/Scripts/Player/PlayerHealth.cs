using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IShootable{

    int currentHealth = 100;
    
    public int Health => currentHealth;

    void Update(){
        if (Input.GetKeyDown(KeyCode.L)){
            ReceiveProjectile(10);
        }
    }

    public void ReceiveProjectile(int damage){
        currentHealth -= damage;
        EventManager.instance.PlayerHealthChanged(currentHealth);
    }
}
