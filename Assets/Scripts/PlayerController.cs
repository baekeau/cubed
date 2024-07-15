using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(InputHandler))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _rotationSpeed = 1.0f;
    private VolumeCameraController _volumeCameraController;
    private InputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Start()
    {
        _volumeCameraController = FindObjectOfType<VolumeCameraController>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void LateUpdate()
    {
        RotateCamera();
    }
    
    private void RotateCamera()
    {
        _volumeCameraController.RotateCamera(_inputHandler.RightStickInput.x * _rotationSpeed, _inputHandler.RightStickInput.y * _rotationSpeed);
    }

    private void MovePlayer()
    {
        // get input for movement 
        float horizontalMovement = _inputHandler.LeftStickInput.x;
        float verticalMovement = _inputHandler.LeftStickInput.y;
        
        // calculate movement direction based on player & camera direction
        Vector3 moveDirection = new Vector3(horizontalMovement, 0, verticalMovement);
        Vector3 cameraDirection = _volumeCameraController.transform.TransformDirection(moveDirection);
        
        // move and rotate player
        transform.Translate(cameraDirection * (_speed * Time.deltaTime), Space.World);
        if (cameraDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(cameraDirection);
        }
        
    }
}
