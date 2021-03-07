using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour{
    [Header("Setup")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] Transform playerCamera;
    [SerializeField] LayerMask ignoreLayerWhenAiming;

    [Header("Settings")]
    [SerializeField] float timeBetweenShots = 0.1f;
    
    [Header("Debug")]
    [SerializeField] Vector3 aimPos;
    [SerializeField] Vector3 normalPos;

    float timer;
    void Start(){
        timer = timeBetweenShots;
    }

    void Update(){
        timer -= Time.deltaTime;
        var canFire = false;
        if (timer <= 0){
            canFire = true;
            timer = timeBetweenShots;
        }

        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }

        if (Input.GetButton("Fire2"))
            transform.localPosition = aimPos;
        else
            transform.localPosition = normalPos;
    }

    RaycastHit hit;
    Vector3 targetPoint;

    void OrientBullet(){
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 100f, ~ignoreLayerWhenAiming)){
            targetPoint = hit.point;
            Debug.Log(hit.collider.gameObject.name);
            Debug.DrawLine(playerCamera.position, hit.point, Color.green, 0.1f);
        }
        else{
            targetPoint = playerCamera.position + playerCamera.forward * 50f;
            Debug.DrawLine(playerCamera.position, playerCamera.position + playerCamera.forward * 100f, Color.red, 0.1f);
        }
    }

    void Shoot(){
        OrientBullet();
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.transform.forward = targetPoint - shootingPoint.position;
    }
}