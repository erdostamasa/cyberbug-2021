using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour{
    [Header("Setup")] [SerializeField] GameObject playerCamera;

    [Header("Settings")] [SerializeField] float cameraSensitivity = 5f;

    Vector2 mouseDelta = Vector2.zero;
    float targetVerticalRotation = 0.0f;
    
    void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate(){
        Vector3 currentRotation = playerCamera.transform.localRotation.eulerAngles;

        float newHorizontal = currentRotation.y + (mouseDelta.x * cameraSensitivity * Time.deltaTime);

        targetVerticalRotation -= ((mouseDelta.y * cameraSensitivity) * Time.deltaTime);
        targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, -90f, 90f);
        
        
        Vector3 newRotation = new Vector3(targetVerticalRotation, newHorizontal, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(newRotation);
    }

    // Read mouse movement
    public void ReceiveMouseDelta(InputAction.CallbackContext context){
        mouseDelta = context.action.ReadValue<Vector2>();
    }
}