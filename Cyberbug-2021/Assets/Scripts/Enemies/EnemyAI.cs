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

    [SerializeField] bool hasGun = false;
    [SerializeField] Transform gun;
    [SerializeField] float shootDistance = 20f;

    public NavMeshAgent agent;
    public Transform playerTransform;
    public LayerMask walkableLayer, playerLayer;
    public float aggroDistance = 15f;

    public float chaseSpeed = 4;
    public float wanderSpeed = 2;

    bool playerInRange = false;

    public Status status;
    //public bool isOnnavmesh;

    Vector3 wanderTarget;
    public float wanderTimer = 1f;
    public float wanderRadius = 3f;

    float timeToWander;
    float timer;


    bool seenPlayer = false;
    Vector3 lastKnownPlayerPosition;

    void Awake(){
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Start(){
        InvokeRepeating(nameof(CheckForPlayer), 0f, Random.Range(1f, 2f));
        wanderTarget = transform.position;
        timer = wanderTimer;
        timeToWander = wanderTimer;
        chaseSpeed = Random.Range(chaseSpeed - 1f, chaseSpeed + 1f);
        wanderSpeed = Random.Range(wanderSpeed - 1f, wanderSpeed + 1f);
    }


    void Update(){
        /*if (gun != null){
            Debug.DrawRay(gun.position, playerTransform.position - gun.position, Color.red);
        }*/

        switch (status){
            case Status.FOLLOWING:
                lastKnownPlayerPosition = playerTransform.position;
                agent.speed = chaseSpeed;
                ChasePlayer(playerTransform.position);
                seenPlayer = true;
                break;
            case Status.OBSTSRUCTED:
                agent.speed = chaseSpeed;
                /*float distanceToTarget = (lastKnownPlayerPosition - transform.position).magnitude;
                if (distanceToTarget <= 2f){
                    status = Status.WANDERING;
                }
                else{
                    ChasePlayer(lastKnownPlayerPosition);    
                }*/
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
            timeToWander = UnityEngine.Random.Range(wanderTimer - 0.1f, wanderTimer + 0.1f);
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            agent.SetDestination(randomDirection);
            timer = 0;
        }
    }

    public bool canShoot = false;

    [SerializeField]LayerMask ignoreLayers;
    
    void CheckForPlayer(){
        float distance = (playerTransform.position - transform.position).magnitude;
        //isOnnavmesh = agent.isOnNavMesh;

        if (hasGun){
            if (distance <= aggroDistance){
                //Check if player is visible
                //Debug.DrawRay(gun.position, playerTransform.position - gun.position, Color.green, 0.5f);
                if (Physics.Raycast(gun.position, playerTransform.position - gun.position, out var hit, 500f, ~ignoreLayers)){
                    //Debug.Log("Collided: " + hit.collider.tag);
                    if (hit.collider.CompareTag("Player")){
                        //Debug.Log("PLAYER VISIBLE");
                        if (distance >= shootDistance){
                            status = Status.FOLLOWING;
                            canShoot = false;
                        }
                        else{
                            status = Status.WANDERING;
                            canShoot = true;
                        }
                    }
                    else{
                        //Debug.Log("player not visible");
                        status = Status.FOLLOWING;
                        canShoot = false;
                    }
                }
                else{
                    //Debug.Log("player not visible");
                    status = Status.FOLLOWING;
                    canShoot = false;
                }
            }
            else{
                status = Status.WANDERING;
                canShoot = false;
            }
        }
        else{
            if (distance <= aggroDistance){
                //NavMeshPath p = new NavMeshPath();
                if (agent.isOnNavMesh && NavMesh.SamplePosition(playerTransform.position, out var hit, 4f, NavMesh.AllAreas)){
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
    }

    float repathTimer = 0f;
    float repathTime = 1f;

    void ChasePlayer(Vector3 destination){
        repathTimer += Time.deltaTime;
        if (repathTimer >= repathTime){
            agent.SetDestination(destination);
            repathTime = 0;
        }
    }
}