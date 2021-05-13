using System;
using UnityEngine;

public class Destructible : MonoBehaviour, IShootable{

    [SerializeField] int maxHealthPoints = 1;
    [SerializeField] int currentHealthPoints;

    void Start(){
        currentHealthPoints = maxHealthPoints;
    }

    public void ReceiveProjectile(int damage){
        currentHealthPoints -= damage;
        if (currentHealthPoints <= 0){
            Destroy(this.gameObject);
        }
    }
}