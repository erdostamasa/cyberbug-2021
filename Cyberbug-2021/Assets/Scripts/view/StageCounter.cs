using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageCounter : MonoBehaviour{

    TextMeshProUGUI text;

    void Awake(){
        text = GetComponent<TextMeshProUGUI>();
        EventManager.instance.onStageCompleted += UpdateText;
    }
    

    void UpdateText(int stage){
        text.text = stage.ToString();
    }
}
