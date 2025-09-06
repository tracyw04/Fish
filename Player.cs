using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private int speed = 50;
	private Vector2 currentVelocity;
	
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
}
