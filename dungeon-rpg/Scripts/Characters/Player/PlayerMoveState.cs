using System;
using DungeonRPG.Scripts.General;
using Godot;

namespace DungeonRPG.Scripts.Characters.Player;

public partial class PlayerMoveState : Node
{
    private Player _player;
    private AnimationPlayer _animationPlayer;
    
    public override void _Ready()
    {
        _player = GetOwner<Player>();
        _animationPlayer = _player.AnimationPlayer;
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if (_player.Direction == Vector2.Zero)
        {
            _player.StateMachine.SwitchState<PlayerIdleState>();
            return;
        }
        
         
        _player.Velocity = new Vector3(_player.Direction.X, 0, _player.Direction.Y);
        _player.Velocity *= 5;

        _player.MoveAndSlide();
        _player.Flip();
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
                _animationPlayer.Play(AnimationConstants.Move);
                SetPhysicsProcess(true);
                SetProcessInput(true);
                break;
            case CustomNotifications.PlayerPhysicsProcess:
                SetPhysicsProcess(false);
                SetProcessInput(false);
                break;
            default:
                return;
        }
        
    }
    
    public override void _Input(InputEvent @event)
    {
        if(Input.IsActionJustPressed(InputConstants.Dash))
        {
            _player.StateMachine.SwitchState<PlayerDashState>();            
        }
    }
}
