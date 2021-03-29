using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IShootable{
    
    [SerializeField] int maxHealth = 3;
    [SerializeField] RagdollController ragdoll;
    
    public int currentHealth;

    void Start(){
        currentHealth = maxHealth;
    }

    public void ReceiveProjectile(){
        currentHealth--;
        if (currentHealth <= 0){
            ragdoll.ToggleRagdoll(true);
        }
    }
}