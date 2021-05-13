using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitZone : MonoBehaviour{
    float damageCooldown = 1f;
    [SerializeField] float knockbackForce = 30f;
    [SerializeField] float upKnockForce = 10f;
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
            canDamage = false;
            //Debug.Log("DAMAGED!");
            PlayerHealth ph = other.gameObject.GetComponent<PlayerHealth>();
            if (ph != null){
                ph.ReceiveProjectile(5);
            }

            Vector3 forceDirection = (other.transform.position - transform.position);
            forceDirection = new Vector3(forceDirection.x, 0, forceDirection.y);
            forceDirection.Normalize();
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * upKnockForce + forceDirection * knockbackForce, ForceMode.Impulse);
            other.gameObject.GetComponent<PlayerMovement>().knockedBack = true;
        }
    }
}