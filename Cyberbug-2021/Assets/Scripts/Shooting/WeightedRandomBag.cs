using System;
using System.Collections.Generic;

class WeightedRandomBag<T>{
    struct Entry{
        public float accumulatedWeight;
        public T item;
    }

    List<Entry> entries = new List<Entry>();
    float accumulatedWeight;
    Random rand = new Random(UnityEngine.Random.Range(0, 1000));

    public void AddEntry(T item, float weight){
        accumulatedWeight += weight;
        entries.Add(new Entry{item = item, accumulatedWeight = accumulatedWeight});
    }

    public T GetRandom(){
        float r = (float)rand.NextDouble() * accumulatedWeight;

        foreach (Entry entry in entries){
            if (entry.accumulatedWeight >= r){
                return entry.item;
            }
        }

        return default(T); //should only happen when there are no entries
    }
}