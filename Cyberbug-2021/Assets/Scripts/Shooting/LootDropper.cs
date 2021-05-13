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
            Vector3 randomOffset = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1f, 1f));
            if (Physics.Raycast(transform.position + randomOffset + Vector3.up * 2, Vector3.down, out var hit, 20f, 1 << 10)){
                Vector3 lootPosition = hit.point;
                Instantiate(lootTable[dropIndex], lootPosition + Vector3.up * 0.5f + randomOffset, Quaternion.Euler(0, Random.Range(0, 360f), 0));
            }
        }
    }
}