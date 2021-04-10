﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour{
    int selectedWeapon = 0;

    public int SelectedWeapon{
        get => selectedWeapon;
        set{
            selectedWeapon = value;
            SwitchWeapon(selectedWeapon);
        }
    }

    public void SwitchWeapon(int selected){
        if (transform.childCount > selected){
            foreach (Transform weapon in transform){
                weapon.gameObject.SetActive(false);
            }
            transform.GetChild(selected).gameObject.SetActive(true);
        }
        selectedWeapon = selected;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            SwitchWeapon(0);
            EventManager.instance.WeaponSwitched(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            SwitchWeapon(1);
            EventManager.instance.WeaponSwitched(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            SwitchWeapon(2);
            EventManager.instance.WeaponSwitched(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            SwitchWeapon(3);
            EventManager.instance.WeaponSwitched(3);
        }
    }
}
