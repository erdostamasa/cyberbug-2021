using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EventManager : MonoBehaviour{

    public static EventManager instance;

    void Awake(){
        instance = this;
    }
    
    public event Action onWeaponSwitched;
    public void WeaponSwitched(){
        onWeaponSwitched?.Invoke();
    }

    public event Action<int, int> onAmmoChanged;
    public void AmmoChanged(int inMagazine, int inInventory){
        onAmmoChanged?.Invoke(inMagazine, inInventory);
    }
}
