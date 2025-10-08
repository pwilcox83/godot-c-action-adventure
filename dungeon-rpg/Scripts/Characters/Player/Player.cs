using System;
using DungeonRPG.Scripts.General;
using Godot;

namespace DungeonRPG.Scripts.Characters.Player;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimationPlayer { get; private set; }
    [Export] public Sprite3D Sprite3d { get; private set; }
    
    public Vector2 Direction = Vector2.Zero;
    
    [Export] public StateMachine StateMachine { get; private set; }

    public override void _Ready()
    {
    }

    public void Flip()
    {
        var isMovingHorizontally = Direction.X != 0;
        if(!isMovingHorizontally) {return;}
        
        var isFacingLeft = Direction.X < 0;
        Sprite3d.FlipH = isFacingLeft;
    }  
    
    public override void _Input(InputEvent @event)
    {
        Direction = Input.GetVector(InputConstants.Left, InputConstants.Right, InputConstants.Forward, InputConstants.Backward);
    }

}
