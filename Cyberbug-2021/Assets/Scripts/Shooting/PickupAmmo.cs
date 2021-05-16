using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmo : MonoBehaviour{
    [SerializeField] Transform pistolT;
    AmmoManager pistol;
    [SerializeField] Transform shotgunT;
    AmmoManager shotgun;
    [SerializeField] Transform rifleT;
    AmmoManager rifle;

    [SerializeField] PlayerHealth healthManager;
    
    [SerializeField] AudioClip hpSound;
    [SerializeField] AudioClip pickupSound;
    
    

    void Start(){
        pistol = pistolT.GetComponent<AmmoManager>();
        shotgun = shotgunT.GetComponent<AmmoManager>();
        rifle = rifleT.GetComponent<AmmoManager>();
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("AmmoLoot")){
            AmmoLoot ammo = other.gameObject.GetComponent<AmmoLoot>();
            AudioClip chosenClip = null;
            switch (ammo.weapon){
                case "Pistol":
                    pistol.AddAmmo(3);
                    chosenClip = pickupSound;
                    break;
                case "Shotgun":
                    shotgun.AddAmmo(1);
                    chosenClip = pickupSound;
                    break;
                case "Rifle":
                    rifle.AddAmmo(10);
                    chosenClip = pickupSound;
                    break;
                case "HP":
                    healthManager.Heal(10);
                    chosenClip = hpSound;
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(chosenClip, transform.position, 0.2f);
        }
    }
}