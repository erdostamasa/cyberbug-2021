using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IShootable{
    [SerializeField] int totalHealth = 1;
    int currentHealth;

    void Start(){
        currentHealth = totalHealth;
    }

    public void ReceiveProjectile(){
        currentHealth--;
        if (currentHealth <= 0){
            Destroy(transform.parent.gameObject);
        }
    }
}