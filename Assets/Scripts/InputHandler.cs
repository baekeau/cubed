using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 LeftStickInput { get; private set; }
    public Vector2 RightStickInput { get; private set; }
    public bool JumpButtonPressed { get; private set; }
    public bool AttackButtonPressed { get; private set; }

    [Required("Input Action Asset is required. Please assign.")]
    [SerializeField] private InputActionAsset _inputActionAsset;
    private InputActionMap _gameplayActionMap;
    private InputAction _moveAction;
    private InputAction _lookAction;

    private void Awake()
    {
        // Get the PlayerInput component (it will be automatically added by the RequireComponent attribute)
        PlayerInput playerInput = GetComponent<PlayerInput>();

        // Set the input action asset programmatically
        playerInput.actions = _inputActionAsset;

        // Enable the PlayerInput component
        playerInput.ActivateInput();

        // Get the gameplay action map
        _gameplayActionMap = playerInput.actions.FindActionMap("Player");

        // Get the move and look actions
        _moveAction = _gameplayActionMap.FindAction("Move");
        _lookAction = _gameplayActionMap.FindAction("Look");
    }

    private void OnEnable()
    {
        _moveAction.performed += OnMoveInput;
        _moveAction.canceled += OnMoveInput;
        _lookAction.performed += OnLookInput;
        _lookAction.canceled += OnLookInput;

        _gameplayActionMap.Enable();
    }

    private void OnDisable()
    {
        _moveAction.performed -= OnMoveInput;
        _moveAction.canceled -= OnMoveInput;
        _lookAction.performed -= OnLookInput;
        _lookAction.canceled -= OnLookInput;

        _gameplayActionMap.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        LeftStickInput = context.ReadValue<Vector2>();
    }

    private void OnLookInput(InputAction.CallbackContext context)
    {
        RightStickInput = context.ReadValue<Vector2>();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        JumpButtonPressed = context.ReadValueAsButton();
    }
    
    private void OnAttack(InputAction.CallbackContext context)
    {
        AttackButtonPressed = context.ReadValueAsButton();
    }
}
