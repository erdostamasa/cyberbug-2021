using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour{
    [Header("Setup")] [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] Transform playerCamera;
    [SerializeField] LayerMask ignoreLayerWhenAiming;
    public GameObject visual;
    public bool selected = false;
    AmmoManager ammo;

    [Header("Settings")] [SerializeField] float timeBetweenShots = 0.1f;

    [Header("Debug")] [SerializeField] Vector3 aimPos;
    [SerializeField] Vector3 normalPos;

    void Awake(){
        ammo = gameObject.GetComponent<AmmoManager>();
    }

    float timer;

    void Start(){
        timer = timeBetweenShots;
    }

    public void Activate(){
        visual.SetActive(true);
        selected = true;
    }

    public void Disable(){
        visual.SetActive(false);
        selected = false;
    }

    bool canFire = true;

    void Update(){
        if (PauseMenu.GameIsPaused) return;
        if (!selected) return;

        if (canFire && Input.GetButton("Fire1")){
            canFire = false;
            Shoot();
            Invoke(nameof(EnableFiring), timeBetweenShots);
        }

        if (Input.GetButton("Fire2"))
            transform.localPosition = aimPos;
        else
            transform.localPosition = normalPos;

        if (Input.GetKeyDown(KeyCode.R)){
            ammo.Reload();
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
            OrientBullet();
            var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            bullet.transform.forward = targetPoint - shootingPoint.position;
            ammo.Fire();
        }
    }
}