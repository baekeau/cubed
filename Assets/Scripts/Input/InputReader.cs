using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
{
    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput != null) return;
        _gameInput = new GameInput();
        _gameInput.Gameplay.SetCallbacks(this);
        _gameInput.UI.SetCallbacks(this);
            
        SetGamePlay();
    }

    public void SetGamePlay()
    {
        _gameInput.Gameplay.Enable();
        _gameInput.UI.Disable();
    }

    public void SetUI()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.UI.Enable();
    }
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    
    public event Action JumpEvent;
    public event Action JumpCancelledEvent;

    public event Action PauseEvent;
    public event Action ResumeEvent;

    public event Action DashEvent;
    
    public event Action L1Event;
    public event Action L2Event;
    public event Action R1Event;
    public event Action R2Event;
    public event Action AttackEvent;
    public event Action TriangleEvent;
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        PauseEvent?.Invoke(); 
        SetUI();
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        ResumeEvent?.Invoke();
        SetGamePlay();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            JumpCancelledEvent?.Invoke();
        }
    }
    
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        DashEvent?.Invoke();
    }

    public void OnL1(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;  
        L1Event?.Invoke();
    }

    public void OnL2(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        L2Event?.Invoke();
    }

    public void OnR1(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        R1Event?.Invoke();
    }

    public void OnR2(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        R2Event?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        AttackEvent?.Invoke();
    }

    public void OnTriangle(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        TriangleEvent?.Invoke();
    }
}
