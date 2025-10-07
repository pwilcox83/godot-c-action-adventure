using System;
using DungeonRPG.Scripts.General;
using Godot;

namespace DungeonRPG.Scripts.Characters.Player;

public partial class PlayerDashState : Node
{
    private Player _player;
    private AnimationPlayer _animationPlayer;
    [Export] private Timer _timer;
    [Export] public float DashSpeed = 10;
    public override void _Ready()
    {
        _player = GetOwner<Player>(); 
        _animationPlayer = _player.AnimationPlayer;
        SetPhysicsProcess(false);
        _timer.Timeout += HandleDashTimeout;
    }

    private void HandleDashTimeout()
    {
        _player.Velocity = Vector3.Zero;

        _player.StateMachine.SwitchState<PlayerIdleState>();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        
        var isEnumDefined = Enum.IsDefined(typeof(CustomNotifications), what);
        if(!isEnumDefined) {return;}
        
        var characterState = (CustomNotifications) what;

        switch (characterState)
        {
            case CustomNotifications.PlayerState:
                _animationPlayer.Play(AnimationConstants.Dash);
                _player.Velocity = new(_player.Direction.X, 0, _player.Direction.Y);

                if (_player.Velocity == Vector3.Zero)
                {
                    _player.Velocity = _player.Sprite3d.FlipH ? Vector3.Left : Vector3.Right;
                }
                _player.Velocity *= DashSpeed;
                _timer.Start();
                SetPhysicsProcess(true);
                break;
            case CustomNotifications.PlayerPhysicsProcess:
                SetPhysicsProcess(false);
                break;
            default:
                return;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _player.MoveAndSlide();
        _player.Flip();
    }
}
