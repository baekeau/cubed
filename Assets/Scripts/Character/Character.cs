using Character.StateMachine;
using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _speed = 3.0f;
        [SerializeField] private float _jumpSpeed = 5.0f;
        private Vector3 _moveDirection;
        private bool _isJumping;
        private ICharacterState _currentState;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            ChangeState(new IdleState());
        }

        public void SetMoveDirection(Vector3 direction)
        {
            _moveDirection = direction;
        }
        
        public void Jump()
        {
            if (_isJumping)
            {
                transform.position += Vector3.up * (_jumpSpeed * Time.deltaTime);
            }
        }

        public void Move()
        {
            if (_moveDirection != Vector3.zero)
            {
                transform.Translate(_moveDirection * (_speed * Time.deltaTime), Space.World);
                transform.rotation = Quaternion.LookRotation(_moveDirection);
                
                if (_currentState is not MovingState)
                {
                    ChangeState(new MovingState());
                }
            } else if (_currentState is not IdleState)
            {
                ChangeState(new IdleState());
            }
        }

        public void ChangeState(ICharacterState newState)
        {
            _currentState?.Exit(this);
            _currentState = newState;
            _currentState.Enter(this);
            Debug.Log($"Changed to state: {newState.GetType().Name}");
        }

        public void UpdateState()
        {
            _currentState?.Update(this);
        }

        public void SetAnimatorParameter(string paramName, bool value)
        {
            _animator.SetBool(paramName, value);
        }

        // what is this?
        public void TriggerAnimation(string triggerName)
        {
           _animator.SetTrigger(triggerName); 
        }

        public void SetJumping(bool isJumping)
        {
            _isJumping = isJumping;
        }
    }
}