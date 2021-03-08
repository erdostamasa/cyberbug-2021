using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IShootable{
    public NavMeshAgent agent;
    public Transform playerTransform;
    public LayerMask walkableLayer, playerLayer;
    
    
    void Awake(){
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update(){
        ChasePlayer();
    }


    void ChasePlayer(){
        agent.SetDestination(playerTransform.position);
    }

    public void ReceiveProjectile(){
        throw new NotImplementedException();
    }
}
