using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour{
    int ammoCount;

    public int AmmoCount{
        get => ammoCount;
        set => ammoCount = value;
    }

    public void Fire(){
        ammoCount--;
    }
}