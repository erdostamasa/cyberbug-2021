using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour{
    [Header("Setup")] [SerializeField] Transform target;

    [Header("Settings")] [SerializeField] Vector3 offset;
    
    void LateUpdate(){
        transform.position = target.position + offset;
    }
}
