using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour{
    int selectedWeapon;

    public int SelectedWapon{
        get => selectedWeapon;
        set => selectedWeapon = value;
    }

    public void SelectWeapon(int selected){
        foreach (Transform weapon in transform){
            weapon.gameObject.SetActive(false);
        }
        transform.GetChild(selected).gameObject.SetActive(true);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            SelectWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            SelectWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            SelectWeapon(2);
        }
    }
}
