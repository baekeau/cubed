using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _jumpSpeed = 5.0f;
    [SerializeField] private float _rotationSpeed = 1.0f;
    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
    
    [SerializeField] private VolumeCameraController _volumeCameraController;

    [SerializeField] private InputReader _input;
    private bool _isJumping;

    private void Start()
    {
        _input.MoveEvent += HandleMove;
        _input.LookEvent += HandleLook;
        _input.JumpEvent += HandleJump;
        _input.JumpCancelledEvent += HandleCancelledJump;
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;
    }

    private void HandleResume()
    {
        // handle resume here
        Debug.Log("The game is resumed");
    }

    private void HandlePause()
    {
        // handle pause here
        Debug.Log("The game is paused");
    }

    private void HandleLook(Vector2 dir)
    {
        _lookDirection = dir;
    }

    private void HandleMove(Vector2 dir)
    {
        _moveDirection = dir;
    }

    private void HandleCancelledJump()
    {
        _isJumping = false;
    }

    private void HandleJump()
    {
        _isJumping = true;
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        if (!_isJumping)
        {
            return;
        }
        transform.position += Vector3.up * (_jumpSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        RotateCamera();
    }
    
    private void Move()
    {
        if (_moveDirection == Vector2.zero)
        {
            return;
        }
        Vector3 moveDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        Vector3 cameraDirection = _volumeCameraController.transform.TransformDirection(moveDirection);
        
        transform.Translate(cameraDirection * (_speed * Time.deltaTime), Space.World);
        if (cameraDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(cameraDirection);
        }
        //transform.position += new Vector3(_moveDirection.x, 0, _moveDirection.y) * (_speed * Time.deltaTime);
    }
    
    private void RotateCamera()
    {
        if (_lookDirection == Vector2.zero)
        {
            return;
        }
        _volumeCameraController.RotateCamera(_lookDirection.x * _rotationSpeed);
    }
}
