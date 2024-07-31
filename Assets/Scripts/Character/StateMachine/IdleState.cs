namespace Character.StateMachine
{
    public class IdleState : ICharacterState
    {
        public void Enter(Character character)
        {
            character.PlayAnimation("Idle");
        }

        public void Update(Character character)
        {
            if (character.IsMoving())
            {
                character.ChangeState(new WalkingState());
            }
        }

        public void Exit(Character character)
        {
            // cleanup if necessary
        }
        
        
        // TODO: what other states to implement?
    }
}