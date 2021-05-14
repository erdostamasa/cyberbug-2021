using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour{
    int ammoLoaded;
    [SerializeField] int ammoInInventory = 0;
    [SerializeField] int magazineSize;
    public int maxAmmoInventory = 999;
    [SerializeField] private float reloadTime=1f;
    private bool isReloading = false;
    [SerializeField] Animator animator;
    [SerializeField] GameObject visual;
    
    [SerializeField] AudioClip reloadSound;
    [SerializeField] float reloadVolume;
    
    [SerializeField] AudioSource audioPlayer;
    
    private void Start()
    {
        ammoLoaded = magazineSize;
    }

    public int MagazineSize{
        set{
            magazineSize = value;
        }
    }

    public int AmmoInInventory{
        get => ammoInInventory;
    }
    
    public bool IsReloding{
        get => isReloading;
    }
    
    void Update(){
        //Debug.Log(ammoLoaded + "/" + ammoInInventory);
    }

    public int AmmoLoaded => ammoLoaded;

    public IEnumerator Reload()
    {
        if ((ammoLoaded != magazineSize) && ammoInInventory!=0)
        {
            isReloading = true;
            WeaponSwitching.instance.isReloading = true;
            animator.SetBool("Reloading",true);
            audioPlayer.PlayOneShot(reloadSound, reloadVolume);
            yield return new WaitForSeconds(reloadTime);
            animator.SetBool("Reloading",false);
            ammoInInventory += ammoLoaded;
            if (ammoInInventory >= magazineSize){
            
                ammoLoaded = magazineSize;
                ammoInInventory -= magazineSize;
            }
            else{
                ammoLoaded = ammoInInventory;
                ammoInInventory = 0;
            }
            isReloading = false;
            WeaponSwitching.instance.isReloading = false;
            EventManager.instance.AmmoChanged(ammoLoaded, ammoInInventory);
        }
    }

    public void AddAmmo(int ammoToAdd){
        int newAmmo = ammoInInventory + ammoToAdd;
        if (newAmmo > maxAmmoInventory){
            ammoInInventory = maxAmmoInventory;
        }
        else{
            ammoInInventory = newAmmo;
        }

        if (visual.activeSelf){
            EventManager.instance.AmmoChanged(ammoLoaded, ammoInInventory);    
        }
    }
    
    public void Fire(){
        if (ammoLoaded > 0){
            ammoLoaded--;
            EventManager.instance.AmmoChanged(ammoLoaded, ammoInInventory);
        }
    }
}