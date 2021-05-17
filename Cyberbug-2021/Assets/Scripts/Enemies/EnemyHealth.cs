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

    Material normalMat;
    [SerializeField] Material hurtMat;
    
    int currentHealth;
    bool dead = false;

    Renderer[] renderers;
    
    public int Health => currentHealth;

    public int MaxHealth{
        get => maxHealth;
        set{
            maxHealth = value;
            currentHealth = maxHealth;
        }
    }

    void Start(){
        renderers = GetComponentsInChildren<Renderer>();
        normalMat = GetComponentInChildren<MeshRenderer>().material;
        currentHealth = maxHealth;
        if (particleObject != null){
            ps = particleObject.GetComponent<ParticleSystem>();
        }
    }

    public void ReceiveProjectile(int damage){
        foreach (Renderer meshRenderer in renderers){
            meshRenderer.material = hurtMat;
        }
        Invoke(nameof(ResetMaterial), 0.1f);
        if (dead) return;
        currentHealth -= damage;
        if (currentHealth <= 0){
            dead = true;
            GetComponent<LootDropper>().DropLoot();
            ragdoll.ToggleRagdoll(true);
            EventManager.instance.EnemyDied();
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

    void ResetMaterial(){
        foreach (Renderer meshRenderer in renderers){
            meshRenderer.material = normalMat;
        }
    }
}