using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 LeftStickInput { get; private set; }
    public Vector2 RightStickInput { get; private set; }

    [Required("Input Action Asset is required. Please assign.")]
    [SerializeField] private InputActionAsset _inputActionAsset;
    
    private InputActionMap _playerActionMap;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;

    private void Awake()
    {
        // Get the PlayerInput component (it will be automatically added by the RequireComponent attribute)
        PlayerInput playerInput = GetComponent<PlayerInput>();

        // Set the input action asset programmatically
        playerInput.actions = _inputActionAsset;

        // Enable the PlayerInput component
        playerInput.ActivateInput();

        // Get the Player action map
        _playerActionMap = playerInput.actions.FindActionMap("Player");

        // Get the move and look actions
        _moveAction = _playerActionMap.FindAction("Move");
        _lookAction = _playerActionMap.FindAction("Look");
        _jumpAction = _playerActionMap.FindAction("Jump");
    }

    private void OnEnable()
    {
        _moveAction.performed += OnMoveInput;
        _moveAction.canceled += OnMoveInput;
        _lookAction.performed += OnLookInput;
        _lookAction.canceled += OnLookInput;

        _playerActionMap.Enable();
    }

    private void OnDisable()
    {
        _moveAction.performed -= OnMoveInput;
        _moveAction.canceled -= OnMoveInput;
        _lookAction.performed -= OnLookInput;
        _lookAction.canceled -= OnLookInput;

        _playerActionMap.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        LeftStickInput = context.ReadValue<Vector2>();
    }

    private void OnLookInput(InputAction.CallbackContext context)
    {
        RightStickInput = context.ReadValue<Vector2>();
    }
    
    
}
