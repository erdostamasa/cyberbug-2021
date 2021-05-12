using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour{
    int ammoLoaded;
    [SerializeField] int ammoInInventory = 0;
    [SerializeField] int magazineSize;
    public int maxAmmoInventory = 999;

    public int MagazineSize{
        set{
            magazineSize = value;
        }
    }

    public int AmmoInInventory{
        get => ammoInInventory;
    }

    void Update(){
        //Debug.Log(ammoLoaded + "/" + ammoInInventory);
    }

    public int AmmoLoaded => ammoLoaded;

    public void Reload(){
        ammoInInventory += ammoLoaded;
        if (ammoInInventory >= magazineSize){
            ammoLoaded = magazineSize;
            ammoInInventory -= magazineSize;
        }
        else{
            ammoLoaded = ammoInInventory;
            ammoInInventory = 0;
        }
        EventManager.instance.AmmoChanged(ammoLoaded, ammoInInventory);
    }

    public void AddAmmo(int ammoToAdd){
        int newAmmo = ammoInInventory + ammoToAdd;
        if (newAmmo > maxAmmoInventory){
            ammoInInventory = maxAmmoInventory;
        }
        else{
            ammoInInventory = newAmmo;
        }
    }
    
    public void Fire(){
        if (ammoLoaded > 0){
            ammoLoaded--;
            EventManager.instance.AmmoChanged(ammoLoaded, ammoInInventory);
        }
    }
}