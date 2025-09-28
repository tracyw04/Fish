using Godot;
using System;

public partial class Fish : Node2D
{
	private AnimationPlayer _animationPlayer;
	
	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.Play("Caught");
		var timer = GetNode<Timer>("Timer");
		timer.Start();
	}
	
	public void OnCooldownTimerTimeout()
	{
		QueueFree();
	}
}
