using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private int speed = 105;
	private Vector2 currentVelocity;
	private AnimationPlayer _animationPlayer;
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		handleInput();
		
		Velocity = currentVelocity;
		MoveAndSlide();
	}
	
	private void handleInput() {
		currentVelocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		currentVelocity *= speed;
	}
	
	 public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}
	
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_left"))
		{
			_animationPlayer.Play("WalkingL");
		} 
		else if (Input.IsActionPressed("ui_right")) 
		{
			_animationPlayer.Play("WalkingR");
		}
		else if (Input.IsActionPressed("ui_up")) 
		{
			_animationPlayer.Play("WalkingU");
		}
		else if (Input.IsActionPressed("ui_down")) 
		{
			_animationPlayer.Play("WalkingF");
		}
		else
		{
			_animationPlayer.Play("Idle");
		}
	}
}
