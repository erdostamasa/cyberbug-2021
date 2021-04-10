using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoAndWeaponUIImage : MonoBehaviour
{
    [SerializeField] GameObject rifleImage;
    [SerializeField] GameObject shotgunImage;
    [SerializeField] GameObject grenadeImage;
    [SerializeField] GameObject pistolImage;
    [SerializeField] TextMeshProUGUI ammo;
    
    void Start()
    {
        EventManager.instance.onAmmoChanged += AmmoUpdate;
        EventManager.instance.onWeaponSwitched += WeaponSwitch;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AmmoUpdate(int ammoLoaded, int ammoInInventory)
    {
        ammo.text = ammoLoaded + "/" + ammoInInventory;
    }

    void WeaponSwitch(int weaponNumber)
    {
        if (weaponNumber == 0)
        {
            rifleImage.SetActive(false);
            shotgunImage.SetActive(false);
            grenadeImage.SetActive(false);
            pistolImage.SetActive(true);
            //ammo.text = ammoLoaded + "/" + ammoInInventory;
        }
        else if (weaponNumber==1)
        {   
            rifleImage.SetActive(false);
            shotgunImage.SetActive(true);
            grenadeImage.SetActive(false);
            pistolImage.SetActive(false);
            //ammo.text = ammoLoaded + "/" + ammoInInventory;
        }
        else if (weaponNumber==2)
        {
            rifleImage.SetActive(true);
            shotgunImage.SetActive(false);
            grenadeImage.SetActive(false);
            pistolImage.SetActive(false);
            //ammo.text = ammoLoaded + "/" + ammoInInventory;
        }
        else if (weaponNumber == 3)
        {
            rifleImage.SetActive(false);
            shotgunImage.SetActive(false);
            grenadeImage.SetActive(true);
            pistolImage.SetActive(false);
            //ammo.text = ammoLoaded + "/" + ammoInInventory;
        }
    }

}
