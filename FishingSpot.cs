using Godot;
using System;

public partial class FishingSpot : Area2D
{
	private Player _playerInside = null;
	PackedScene _fishScene;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is Player player) // or body.IsInGroup("Player")
		{
			_playerInside = player;
		}
	}

	private void OnBodyExited(Node body)
	{
		if (body is Player)
		{
			_playerInside = null;
		}
	}

	public override void _Process(double delta)
	{
		if (_playerInside != null && Input.IsActionJustPressed("Fish"))
		{
			GD.Print("You caught a Fish!");
			_fishScene = GD.Load<PackedScene>("res://scenes//fish.tscn");
			 SpawnFish();
			// Do whatever you want: open door, show dialog, trigger cutscene, etc.
		}
	}
	
	private void SpawnFish()
	{
		var fish = (Node2D)_fishScene.Instantiate();
		GetParent().AddChild(fish);
		Vector2 fish_position = new Vector2(GlobalPosition.X, _playerInside.GlobalPosition.Y);
		fish.GlobalPosition = fish_position; 
		//GD.Print("Fish spawned at ", fish.GlobalPosition);
	}
}
