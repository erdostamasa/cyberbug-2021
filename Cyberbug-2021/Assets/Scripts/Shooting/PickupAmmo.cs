﻿using System;
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
    [SerializeField] Transform grenadeT;
    AmmoManager grenade;

    [SerializeField] AudioClip pickupSound;
    
    

    void Start(){
        pistol = pistolT.GetComponent<AmmoManager>();
        shotgun = shotgunT.GetComponent<AmmoManager>();
        rifle = rifleT.GetComponent<AmmoManager>();
        grenade = grenadeT.GetComponent<AmmoManager>();
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("AmmoLoot")){
            AmmoLoot ammo = other.gameObject.GetComponent<AmmoLoot>();
            switch (ammo.weapon){
                case "Pistol":
                    pistol.AddAmmo(1);
                    break;
                case "Shotgun":
                    shotgun.AddAmmo(1);
                    break;
                case "Rifle":
                    rifle.AddAmmo(1);
                    break;
                case "Greande":
                    grenade.AddAmmo(1);
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 0.2f);
        }
    }
}