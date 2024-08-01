using UnityEngine;

namespace Character.StateMachine
{
    public class IdleState : ICharacterState
    {
        public void Enter(Character character)
        {
            character.SetAnimatorParameter("isMoving", false);
            character.SetAnimatorParameter("isAttacking", false);
            Debug.Log("Entered Idle State");
        }

        public void Update(Character character)
        {
            // check for state transitions
        }

        public void Exit(Character character)
        {
            // cleanup if necessary
        }
        
    }
}