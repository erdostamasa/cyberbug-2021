﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [Header("Setup")] [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] Transform playerCamera;
    [SerializeField] LayerMask ignoreLayerWhenAiming;
    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] AudioClip shootSound;
    AudioSource audioPlayer;
    public GameObject visual;
    public bool selected = false;
    AmmoManager ammo;

    [Header("Settings")] [SerializeField] float timeBetweenShots = 0.1f;
    
    void Awake()
    {
        ammo = gameObject.GetComponent<AmmoManager>();
    }

    float timer;

    [SerializeField] float randomSpread = 1f;
    [SerializeField] int projectilePerShot = 1;

    void Start(){
        timer = timeBetweenShots;
        audioPlayer = GetComponent<AudioSource>();
    }

    public void Activate(){
        visual.SetActive(true);
        selected = true;
        EventManager.instance.AmmoChanged(ammo.AmmoLoaded, ammo.AmmoInInventory);
    }

    public void Disable(){
        visual.SetActive(false);
        selected = false;
    }

    bool canFire = true;

    void Update()
    {
        if (ammo.IsReloding) return;
        if (PauseMenu.GameIsPaused) return;
        if (!selected) return;

        if (canFire && Input.GetButton("Fire1")){
            canFire = false;
            Shoot();
            Invoke(nameof(EnableFiring), timeBetweenShots);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ammo.Reload());
        }
    }

    void EnableFiring(){
        canFire = true;
    }

    RaycastHit hit;
    Vector3 targetPoint;

    void OrientBullet(){
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 100f, ~ignoreLayerWhenAiming)){
            targetPoint = hit.point;
        }
        else{
            targetPoint = playerCamera.position + playerCamera.forward * 50f;
        }
    }

    void Shoot(){
        if (ammo.AmmoLoaded > 0){
            muzzleFlash.Play();
            OrientBullet();
            
            for (int i = 0; i < projectilePerShot; i++){
                var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
                bullet.transform.forward = targetPoint - shootingPoint.position;
                float rx = Random.Range(-randomSpread, randomSpread);
                float ry = Random.Range(-randomSpread, randomSpread);
                float rz = Random.Range(-randomSpread, randomSpread);
                bullet.transform.Rotate(rx, ry, rz);    
            }
            
            ammo.Fire();
            audioPlayer.PlayOneShot(shootSound, Random.Range(0.23f, 0.26f));
        }
    }
}
