using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform orientation;
    [SerializeField] private Vector2 sensitivity = new Vector2(1f, 1f);

    private float targetAngleX = 0.0f;

    void Update(){
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void LateUpdate(){
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 currentRotation = playerCamera.localRotation.eulerAngles;

        // Calculate horizontal target
        float targetAngleY = currentRotation.y + (mouseDelta.x * sensitivity.x);

        // Calculate vertical target
        targetAngleX -= (mouseDelta.y * sensitivity.y);
        targetAngleX = Mathf.Clamp(targetAngleX, -90f, 90f);

        // Apply rotation
        playerCamera.localRotation = Quaternion.Euler(targetAngleX, targetAngleY, 0);
        orientation.localRotation = Quaternion.Euler(0, targetAngleY, 0);
    }
}