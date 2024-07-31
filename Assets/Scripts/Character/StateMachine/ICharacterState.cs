namespace Character.StateMachine
{
    public interface ICharacterState
    {
        void Enter(Character character);
        void Update(Character character);
        void Exit(Character character);
    }
}