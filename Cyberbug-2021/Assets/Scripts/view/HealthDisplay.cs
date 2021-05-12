using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour{
    TextMeshProUGUI health;

    void Start(){
        EventManager.instance.onPlayerHealthChanged += DisplayHealth;
        health = GetComponent<TextMeshProUGUI>();
    }

    void DisplayHealth(int hp){
        health.text = hp.ToString();
    }
}