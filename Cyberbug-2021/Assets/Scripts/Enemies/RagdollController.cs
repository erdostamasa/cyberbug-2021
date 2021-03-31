using System;
using System.Collections;
using System.Collections.Generic;
using DitzelGames.FastIK;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RagdollController : MonoBehaviour{
    [SerializeField] float deathForce = 10f;
    [SerializeField] float deathRotation = 5f;
    
    Collider[] childrenCollider;
    Rigidbody[] childrenRigidbody;
    FastIKFabric[] childrenIK;
    
    void Awake(){
        childrenCollider = GetComponentsInChildren<Collider>();
        childrenRigidbody = GetComponentsInChildren<Rigidbody>();
        childrenIK = GetComponentsInChildren<FastIKFabric>();
    }
    
    void Start(){
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool ragdollActive){
        GetComponent<NavMeshAgent>().enabled = !ragdollActive;
        GetComponent<EnemyAI>().enabled = !ragdollActive;
        GetComponent<AudioSource>().enabled = !ragdollActive;

        
        foreach (FastIKFabric child in childrenIK){
            child.enabled = !ragdollActive;
        }
        
        foreach (Collider collider in childrenCollider){
            collider.enabled = ragdollActive;
        }

        foreach (Rigidbody rigidbody in childrenRigidbody){
            rigidbody.detectCollisions = ragdollActive;
            rigidbody.isKinematic = !ragdollActive;
            rigidbody.AddTorque(Vector3.one * Random.Range(deathRotation, deathRotation), ForceMode.Acceleration);
            rigidbody.AddForce(Vector3.up * Random.Range(deathForce-50, deathForce+50), ForceMode.Acceleration);
        }

        GetComponent<Collider>().enabled = !ragdollActive;
        GetComponent<Rigidbody>().detectCollisions = !ragdollActive;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}