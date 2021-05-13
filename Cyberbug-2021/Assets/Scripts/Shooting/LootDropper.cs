using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour{

    [SerializeField] int minDrop;
    [SerializeField] int maxDrop;

    [SerializeField] List<GameObject> lootTable;
    
    public void DropLoot(){
        int dropCount = Random.Range(minDrop, maxDrop);
        for (int i = 0; i < dropCount; i++){
            int dropIndex = Random.Range(0, lootTable.Count);
            if (Physics.Raycast(transform.position + Vector3.up*2, Vector3.down,  out var hit, 20f, 1 << 10)){
                Vector3 lootPosition = hit.point;
                Instantiate(lootTable[dropIndex], lootPosition + Vector3.up *0.5f, Quaternion.identity);    
            }
        }
    }
}
