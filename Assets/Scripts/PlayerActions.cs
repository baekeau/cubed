using UnityEngine;

public class MoveAction : IPlayerAction
{
    public void Execute(PlayerController player)
    {
        player.Move();
    }
}

public class JumpAction : IPlayerAction
{
    public void Execute(PlayerController player)
    {
        player.Jump();
    }
}

public class AttackAction : IPlayerAction
{
    public void Execute(PlayerController player)
    {
        player.Attack();
    }
}