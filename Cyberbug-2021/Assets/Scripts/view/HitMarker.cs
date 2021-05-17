using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HitMarker : MonoBehaviour{
    [SerializeField] SpriteRenderer hitMarker;
    [SerializeField] float hitMarkerTime = 0.1f;
    AudioSource soundPlayer;
    [SerializeField] AudioClip hitSound;
    [SerializeField] float volume = 0.5f;

    void Start(){
        soundPlayer = GetComponent<AudioSource>();
        EventManager.instance.onEnemyHit += EnableHitMarker;
    }

    void EnableHitMarker(){
        hitMarker.enabled = true;
        soundPlayer.PlayOneShot(hitSound, Random.Range(volume - 0.02f, volume + 0.02f));
        Invoke(nameof(DisableHitMarker), hitMarkerTime);
    }

    void DisableHitMarker(){
        hitMarker.enabled = false;
    }
}