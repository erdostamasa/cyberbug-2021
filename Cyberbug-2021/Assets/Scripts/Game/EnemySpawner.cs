using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour{
    [SerializeField] GameObject spiderPrefab;
    int enemiesAlive = 0;
    int stage = 1;

    [SerializeField] int enemiesPerStage = 10;
    [SerializeField] int maxEnemies = 10;

    void Awake(){
        EventManager.instance.onEnemyDied += decreaseEnemyCount;
    }

    void Start(){
        GetComponent<MeshRenderer>().enabled = false;
        //StartCoroutine(SpawnSpiders(5));
    }

    void Update(){
        if (enemiesAlive == 0){
            SpawnSpiders(enemiesPerStage * stage);
            stage++;
        }
    }

    void decreaseEnemyCount(){
        enemiesAlive--;
    }
 
    void SpawnSpiders(int count){
        for (int i = 0; i < count && i < maxEnemies; i++){
            Instantiate(spiderPrefab, transform.position, Quaternion.identity);
            enemiesAlive++;
        }
    }
}