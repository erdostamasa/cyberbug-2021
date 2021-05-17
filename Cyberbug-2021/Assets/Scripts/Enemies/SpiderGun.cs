using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiderGun : MonoBehaviour{
    Transform playerTransform;
    AudioSource _audioSource;

    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject projectile;
    [SerializeField] AudioClip shootSound;
    [SerializeField] EnemyAI ai;
    [SerializeField] float randomSpread = 6f;

    void Start(){
        _audioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        InvokeRepeating(nameof(Shoot), 1f, Random.Range(0.6f, 1f));
    }

    void Update(){
        transform.LookAt(playerTransform.position);
    }

    void Shoot(){
        if (ai.canShoot){
            Transform bullet = Instantiate(projectile, shootPoint.position, shootPoint.rotation).transform;
            bullet.transform.forward = playerTransform.position - shootPoint.position;

            float rx = Random.Range(-randomSpread, randomSpread);
            float ry = Random.Range(-randomSpread, randomSpread);
            float rz = Random.Range(-randomSpread, randomSpread);
            bullet.transform.Rotate(rx, ry, rz);

            _audioSource.PlayOneShot(shootSound, 2f);
        }
    }
}