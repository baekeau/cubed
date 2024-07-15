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
    private Rigidbody _rigidbody;
    private Dictionary<string, IPlayerAction> _actions;
    

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        _rigidbody = GetComponent<Rigidbody>();
        InitializeActions();
    }

    private void InitializeActions()
    {
        _actions = new Dictionary<string, IPlayerAction>
        {
            { "Move", new MoveAction() },
            { "Jump", new JumpAction() },
            { "Attack", new AttackAction() }
        };
    }

    private void ExecuteAction(string actionName)
    {
        if (_actions.TryGetValue(actionName, out IPlayerAction action))
        {
            action.Execute(this);
        }
        
    }
    private void Start()
    {
        _volumeCameraController = FindObjectOfType<VolumeCameraController>();
    }

    private void Update()
    {
        ExecuteAction("Move");
        if (_inputHandler.JumpButtonPressed)
        {
            ExecuteAction("Jump");
        }
        if (_inputHandler.AttackButtonPressed)
        {
            ExecuteAction("Attack");
        }
    }

    private void LateUpdate()
    {
        RotateCamera();
    }
    
    public void Move()
    {
        // get input for movement 
        float horizontalMovement = _inputHandler.LeftStickInput.x;
        float verticalMovement = _inputHandler.LeftStickInput.y;
        
        // calculate movement direction based on player & camera direction
        Vector3 moveDirection = new Vector3(horizontalMovement, 0, verticalMovement);
        Vector3 cameraDirection = _volumeCameraController.transform.TransformDirection(moveDirection);
        
        // move the player
        transform.Translate(cameraDirection * (_speed * Time.deltaTime), Space.World);
        if (cameraDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(cameraDirection);
        }
    }

    public void Jump()
    {
        Debug.Log("Player is Jumping.");
        _rigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    public void Attack()
    {
        Debug.Log("Player is attacking.");
        // Implement attack logic here
    }
    
    private void RotateCamera()
    {
        _volumeCameraController.RotateCamera(_inputHandler.RightStickInput.x * _rotationSpeed, _inputHandler.RightStickInput.y * _rotationSpeed);
    }


}
