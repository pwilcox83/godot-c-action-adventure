using System;
using DungeonRPG.Scripts.General;
using Godot;

namespace DungeonRPG.Scripts.Characters.Player;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "0,20,0.1")] private float _characterSpeed = 5;
    public override void _PhysicsProcess(double delta)
    {
        if (Character.Direction == Vector2.Zero)
        {
            Character.StateMachine.SwitchState<PlayerIdleState>();
            return;
        }
        
         
        Character.Velocity = new Vector3(Character.Direction.X, 0, Character.Direction.Y);
        Character.Velocity *= _characterSpeed;

        Character.MoveAndSlide();
        Character.Flip();
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
        AnimationPlayer.Play(AnimationConstants.Move);
    }
}
