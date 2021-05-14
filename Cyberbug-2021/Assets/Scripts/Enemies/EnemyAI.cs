using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour{
    public enum Status{
        WANDERING,
        FOLLOWING,
        OBSTSRUCTED
    }

    public NavMeshAgent agent;
    public Transform playerTransform;
    public LayerMask walkableLayer, playerLayer;
    public float aggroDistance = 15f;

    public float chaseSpeed = 4;
    public float wanderSpeed = 2;

    bool playerInRange = false;
    public Status status;

    Vector3 wanderTarget;
    public float wanderTimer = 1f;
    public float wanderRadius = 3f;

    float timeToWander;
    float timer;

    void Awake(){
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Start(){
        InvokeRepeating(nameof(CheckForPlayer), 0f, Random.Range(1f, 2f));
        wanderTarget = transform.position;
        timer = wanderTimer;
        timeToWander = wanderTimer;
    }


    void Update(){
        switch (status){
            case Status.FOLLOWING:
                agent.speed = chaseSpeed;
                ChasePlayer();
                break;
            case Status.OBSTSRUCTED:
                agent.speed = chaseSpeed;
                Wander();
                break;
            case Status.WANDERING:
                agent.speed = wanderSpeed;
                Wander();
                break;
        }
    }


    void Wander(){
        timer += Time.deltaTime;
        if (timer >= timeToWander){
            timeToWander = UnityEngine.Random.Range(wanderTimer - 0.5f, wanderTimer + 0.5f);
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            agent.SetDestination(randomDirection);
            timer = 0;
        }
    }

    void CheckForPlayer(){
        float distance = (playerTransform.position - transform.position).magnitude;
        if (distance <= aggroDistance){
            NavMeshPath p = new NavMeshPath();
            if (agent.isOnNavMesh && agent.CalculatePath(playerTransform.position, p)){
                status = Status.FOLLOWING;
            }
            else{
                status = Status.OBSTSRUCTED;
            }
        }
        else{
            status = Status.WANDERING;
        }
    }

    void ChasePlayer(){
        agent.SetDestination(playerTransform.position);
    }
}