using Godot;
using System;

public partial class FishingSpot : Area2D
{
private bool _playerInside = false;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is Player) // or body.IsInGroup("Player")
		{
			_playerInside = true;
		}
	}

	private void OnBodyExited(Node body)
	{
		if (body is Player)
		{
			_playerInside = false;
		}
	}

	public override void _Process(double delta)
	{
		if (_playerInside && Input.IsActionJustPressed("Fish"))
		{
			GD.Print("Player interacted with tile!");
			// Do whatever you want: open door, show dialog, trigger cutscene, etc.
		}
	}
}
