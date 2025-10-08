using System;
using DungeonRPG.Scripts.General;
using Godot;

namespace DungeonRPG.Scripts.Characters.Player;

public partial class PlayerIdleState : PlayerState      
{
    public override void _PhysicsProcess(double delta)
    {
        if (Character.Direction != Vector2.Zero)
        {
            Character.StateMachine.SwitchState<PlayerMoveState>();
        }
    }

  

    public override void _Input(InputEvent @event)
    {
        if(Input.IsActionJustPressed(InputConstants.Dash))
        {
            Character.StateMachine.SwitchState<PlayerDashState>();            
        }
    }

    protected override void EnterState()
    {
        AnimationPlayer.Play(AnimationConstants.Idle);
    }
}
