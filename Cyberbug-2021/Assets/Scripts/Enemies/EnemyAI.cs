using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class EnemyAI : MonoBehaviour{
    public NavMeshAgent agent;
    public Transform playerTransform;
    public LayerMask walkableLayer, playerLayer;
    public float aggroDistance = 15f;

    public float chaseSpeed = 4;
    public float wanderSpeed = 2;
    
    bool playerInRange = false;
    

    Vector3 wanderTarget;
    public float wanderTimer = 1f;
    public float wanderRadius = 3f;

    float timeToWander;
    float timer;

    void Awake(){
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Start(){
        InvokeRepeating(nameof(CheckForPlayer), 0f, 1f);
        wanderTarget = transform.position;
        timer = wanderTimer;
        timeToWander = wanderTimer;
    }

    void Update(){
        if (playerInRange){
            ChasePlayer();
        }
        else{
            Wander();
        }
    }


    void Wander(){
        timer += Time.deltaTime;

        if (timer >= timeToWander){
            timeToWander = UnityEngine.Random.Range(wanderTimer - 0.5f, wanderTimer + 0.5f);
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            //Debug.Log(randomDirection);

            /*NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, 2f, -1);
            wanderTarget = navHit.position;*/


            //Debug.Log(wanderTarget);
            agent.SetDestination(randomDirection);

            timer = 0;
        }
    }

    void CheckForPlayer(){
        float distance = (playerTransform.position - transform.position).magnitude;
        if (distance <= aggroDistance){
            playerInRange = true;
            agent.speed = chaseSpeed;
        }
        else{
            playerInRange = false;
            agent.speed = wanderSpeed;
        }
    }

    void ChasePlayer(){
        agent.SetDestination(playerTransform.position);
    }
}