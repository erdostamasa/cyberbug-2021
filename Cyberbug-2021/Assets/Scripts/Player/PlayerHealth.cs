using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IShootable{

    int currentHealth = 100;
    
    public int Health => currentHealth;
    
    public void ReceiveProjectile(){
        currentHealth--;
    }
}
