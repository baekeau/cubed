using Character.StateMachine;
using Input;
using UnityEngine;

namespace Character
{
    // Handles input
    // Handles game flow (pause, resume, etc.)
    // UI interaction
    // 
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 1.0f;
        [SerializeField] private VolumeCameraController _volumeCameraController;
        [SerializeField] private InputReader _input;
        [SerializeField] private Vector2 _moveDirection;
        [SerializeField] private Vector2 _lookDirection;
        private Character _character;

        private void Start()
        {
            // Subscribe to input events
            _input.MoveEvent += HandleMove;
            _input.LookEvent += HandleLook;
            _input.JumpEvent += HandleJump;
            _input.JumpCancelledEvent += HandleCancelledJump;
            _input.PauseEvent += HandlePause;
            _input.ResumeEvent += HandleResume;
            _input.DashEvent += HandleDash;
            _input.L1Event += HandleL1;
            _input.L2Event += HandleL2;
            _input.R1Event += HandleR1;
            _input.R2Event += HandleR2;
            _input.AttackEvent += HandleAttack;
            _input.TriangleEvent += HandleTriangle;
            
            // TODO: Clean this up, maybe move to VCController
            _character = GetComponent<Character>();
            if (_volumeCameraController != null) return;
            _volumeCameraController = FindObjectOfType<VolumeCameraController>();
            if (_volumeCameraController == null)
            {
                Debug.LogError("VolumeCameraController not found!");
            }

        }
        private void FixedUpdate()
        {
            UpdateCharacterMovement();
            _character.UpdateState();
        }
        
        private void LateUpdate()
        {
            RotateCamera();
        }
        
        private void UpdateCharacterMovement()
        {
            Vector3 moveDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y);
            Vector3 cameraDirection = _volumeCameraController.transform.TransformDirection(moveDirection);
            _character.SetMoveDirection(cameraDirection);
            _character.Move();
            _character.Jump();
        }

        private void RotateCamera()
        {
            if (_lookDirection == Vector2.zero)
            {
                return;
            }

            _volumeCameraController.RotateCamera(_lookDirection.x * _rotationSpeed);
        }
        
        private void HandleTriangle()
        {
            // handle triangle here
            Debug.Log("Triangle");
        }

        private void HandleAttack()
        {
            // handle attack here
            Debug.Log("Attack");
        }

        private void HandleR2()
        {
            // handle R2 here
            Debug.Log("R2");
        }

        private void HandleR1()
        {
            // handle R1 here
            Debug.Log("R1");
        }

        private void HandleL2()
        {
            // handle L2 here
            Debug.Log("L2");
        }

        private void HandleL1()
        {
            // handle L1 here
            Debug.Log("L1");
        }

        private void HandleDash()
        {
            // handle dash here
            Debug.Log("Dash");
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
            _character.SetJumping(false);
            Debug.Log("Jump cancelled");

        }

        private void HandleJump()
        {
            _character.SetJumping(true);
            Debug.Log("Jump button pressed");
        }

       
    }

}