using Character.StateMachine;

namespace Character
{
    public class Character
    {
        private ICharacterState currentState;
        
        public void ChangeState(ICharacterState newState)
        {
            currentState?.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }

        public void Update()
        {
            currentState?.Update(this);
        }

        public void PlayAnimation(string animationName)
        {
            
        }

        public bool IsMoving()
        {
            return true;
        }
    }
}