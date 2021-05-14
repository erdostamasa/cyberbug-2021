using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public static WeaponSwitching instance;
    int selectedWeapon = 0;
    int helpSelected=0;
    public bool isReloading;

    public int SelectedWeapon{
        get => selectedWeapon;
        set{
            selectedWeapon = value;
            SwitchWeapon(selectedWeapon);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start(){
        SwitchWeapon(0);
        EventManager.instance.WeaponSwitched(0);
    }

    public void SwitchWeapon(int selected){
        if (transform.childCount > selected){
            foreach (Transform weapon in transform){
                weapon.GetComponent<Gun>().Disable();
            }
            transform.GetChild(selected).GetComponent<Gun>().Activate();
        }
        selectedWeapon = selected;
        helpSelected = selected;
    }

    void Update()
    {
        if (isReloading) return;
        
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

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (helpSelected >= transform.childCount - 1)
            {
                helpSelected=0;
            }
            else
            {
                helpSelected++;
            }
            SwitchWeapon(helpSelected);
            EventManager.instance.WeaponSwitched(helpSelected);
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (helpSelected <= 0)
            {
                helpSelected = transform.childCount - 1;
            }
            else
            {
                helpSelected--;
            }
            SwitchWeapon(helpSelected);
            EventManager.instance.WeaponSwitched(helpSelected);
        }
    }
}
