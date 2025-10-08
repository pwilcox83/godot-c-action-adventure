using System;
using DungeonRPG.Scripts.General;
using Godot;

namespace DungeonRPG.Scripts.Characters.Player;

public abstract partial class PlayerState : Node
{
    protected Player Character;
    protected AnimationPlayer AnimationPlayer;
    
    public override void _Ready()
    {
        Character = GetOwner<Player>();
        AnimationPlayer = Character.AnimationPlayer;
        SetPhysicsProcess(false);
        SetProcessInput(false);
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
                EnterState();
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
    
    
    protected virtual void EnterState()
    {
        
    }
    
}
