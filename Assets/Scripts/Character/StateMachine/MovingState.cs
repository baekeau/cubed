namespace Character.StateMachine
{
    public class MovingState : ICharacterState
    {
        public void Enter(Character character)
        {
            character.SetAnimatorParameter("isMoving", true);
        }

        public void Update(Character character)
        {
            // Handle moving logic and check for transitions
        }

        public void Exit(Character character)
        {
            character.SetAnimatorParameter("isMoving", false);
        }
    }
}