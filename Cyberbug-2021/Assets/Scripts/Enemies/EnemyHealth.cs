using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour, IShootable{
    [SerializeField] int maxHealth = 3;
    [SerializeField] RagdollController ragdoll;
    [SerializeField] float explodeDelay = 10f;
    [SerializeField] GameObject particleObject;
    [SerializeField] AudioClip explosionSound;
    ParticleSystem ps;
    
    int currentHealth;

    public int Health => currentHealth;

    public int MaxHealth{
        get => maxHealth;
        set{
            maxHealth = value;
            currentHealth = maxHealth;
        }
    }

    void Start(){
        currentHealth = maxHealth;
        ps = particleObject.GetComponent<ParticleSystem>();
    }

    public void ReceiveProjectile(int damage){
        currentHealth -= damage;
        if (currentHealth <= 0){
            GetComponent<LootDropper>().DropLoot();
            ragdoll.ToggleRagdoll(true);
            Invoke(nameof(ExplodeSelf), explodeDelay);
        }
    }

    void ExplodeSelf(){
        particleObject.transform.parent = null;
        ps.Play();
        AudioSource.PlayClipAtPoint(explosionSound, transform.position, Random.Range(0.9f, 1f));
        Destroy(particleObject, 5f);
        Destroy(gameObject);
    }
}