using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IShootable{
    [SerializeField] int maxHealth = 3;
    [SerializeField] RagdollController ragdoll;

    int currentHealth;

    public int Health => currentHealth;

    public int MaxHealth{
        get => maxHealth;
        set{
            maxHealth = value;
            currentHealth = maxHealth;
        }
    }

    void Start(){
        currentHealth = maxHealth;
    }

    public void ReceiveProjectile(int damage){
        currentHealth -= damage;
        if (currentHealth <= 0){
            ragdoll.ToggleRagdoll(true);
        }
    }
}