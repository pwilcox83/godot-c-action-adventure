using System;
using DungeonRPG.Scripts.General;
using Godot;

namespace DungeonRPG.Scripts.Characters.Player;

public partial class PlayerDashState : PlayerState      
{
    [Export] private Timer _timer;
    [Export(PropertyHint.Range, "0,20,0.1" )] public float DashSpeed = 10;
    
    public override void _Ready()
    {
        base._Ready();
        _timer.Timeout += HandleDashTimeout;
    }

    private void HandleDashTimeout()
    {
        Character.Velocity = Vector3.Zero;
        Character.StateMachine.SwitchState<PlayerIdleState>();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        Character.MoveAndSlide();
        Character.Flip();
    }
    
    protected override void EnterState()
    {
        AnimationPlayer.Play(AnimationConstants.Dash);
        Character.Velocity = new(Character.Direction.X, 0, Character.Direction.Y);

        if (Character.Velocity == Vector3.Zero)
        {
            Character.Velocity = Character.Sprite3d.FlipH ? Vector3.Left : Vector3.Right;
        }
        Character.Velocity *= DashSpeed;
        _timer.Start();
    }
}
