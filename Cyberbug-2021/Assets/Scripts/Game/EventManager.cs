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

    public event Action<int> onStageCompleted;
    public void StageCompleted(int newStage){
        onStageCompleted?.Invoke(newStage);
    }
    
    public event Action onEnemyDied;
    public void EnemyDied(){
        onEnemyDied?.Invoke();
    }
    
    public event Action<int> onPlayerHealthChanged;
    public void PlayerHealthChanged(int newHealth){
        onPlayerHealthChanged?.Invoke(newHealth);
    }
    
    public event Action<int> onWeaponSwitched;
    public void WeaponSwitched(int weaponNumber){
        onWeaponSwitched?.Invoke(weaponNumber);
    }

    public event Action<int, int> onAmmoChanged;
    public void AmmoChanged(int inMagazine, int inInventory){
        onAmmoChanged?.Invoke(inMagazine, inInventory);
    }

    public event Action<bool> onMapChanged;
    public void MapChanged(bool enter)
    {
        onMapChanged?.Invoke(enter);
    }
}
