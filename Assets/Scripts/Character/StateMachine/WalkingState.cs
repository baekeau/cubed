namespace Character.StateMachine
{
    public class WalkingState : ICharacterState
    {
        public void Enter(Character character)
        {
            character.PlayAnimation("Walking");
        }

        public void Update(Character character)
        {
            // walking logic
        }

        public void Exit(Character character)
        {
            // cleanup if necessary
        }
    }
}