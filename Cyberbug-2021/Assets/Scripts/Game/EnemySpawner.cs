using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour{
    [SerializeField] GameObject spiderPrefab;
    int enemiesAlive = 0;
    int stage = 1;

    bool startingStage = false;
    
    [SerializeField] int enemiesPerStage = 10;
    [SerializeField] int maxEnemies = 3;

    [SerializeField] float enemyAggroDistance = 50f;

    public List<Transform> spawnLocations;

    [SerializeField] bool infiniteSpawn = true;

    [SerializeField] bool timed = false;
    

    void Awake(){
        EventManager.instance.onEnemyDied += decreaseEnemyCount;
    }

    void Start(){
        GetComponent<MeshRenderer>().enabled = false;
        //StartCoroutine(SpawnSpiders(5));
        EventManager.instance.StageCompleted(stage);
        SpawnSpiders(enemiesPerStage);
    }

    float spawnTimer = 0;
    [SerializeField] float timeToSpawn = 1f;
    //[SerializeField] int spawnCount = 1;
    void Update(){
        spawnTimer += Time.deltaTime;
        if (timed && spawnTimer >= timeToSpawn && enemiesAlive < maxEnemies){
            SpawnSpiders(1);
            spawnTimer = 0;
        }
        if (infiniteSpawn && enemiesAlive == 0){
            NextStage();
        }
    }

    float timer = 0;
    float stageCooldown = 1f;
    void NextStage(){
        
        timer += Time.deltaTime;
        if (timer >= stageCooldown){
            SpawnSpiders(enemiesPerStage * stage);
            stage++;
            timer = 0;
        }
        EventManager.instance.StageCompleted(stage);
    }
    
    void decreaseEnemyCount(){
        enemiesAlive--;
    }
 
    void SpawnSpiders(int count){
        for (int i = 0; i < count && i < maxEnemies; i++){
            Vector3 spawnPosition = spawnLocations[Random.Range(0, spawnLocations.Count)].position;
            Instantiate(spiderPrefab, spawnPosition, Quaternion.identity).GetComponent<EnemyAI>().aggroDistance = enemyAggroDistance;
            enemiesAlive++;
        }
    }
}