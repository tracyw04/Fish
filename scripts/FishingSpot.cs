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
		if (body is Player player) 
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
		if (_playerInside != null && Input.IsActionJustPressed("Fish") && _playerInside.getHasFishingRod())
		{
			_fishScene = GD.Load<PackedScene>("res://scenes//fish.tscn");
			 SpawnFish();
		}
	}
	
	private void SpawnFish()
	{
		var fish = (Fish)_fishScene.Instantiate();
		GetParent().AddChild(fish);
		_playerInside.addToBag(fish);

		Vector2 fish_position;
		var facingDir = _playerInside.FacingDirection;
		//facingDir == Player.Direction.Right
		if (_playerInside.GlobalPosition.X >= 60)
		{
			fish_position = new Vector2(_playerInside.GlobalPosition.X, _playerInside.GlobalPosition.Y + 5);
			//top quadrantf
		}
		else if (_playerInside.GlobalPosition.Y <= -95)
		{
			fish_position = new Vector2(_playerInside.GlobalPosition.X, _playerInside.GlobalPosition.Y);
			//bottom quadrant
		}
		else if (_playerInside.GlobalPosition.Y >= 24)
		{
			fish_position = new Vector2(_playerInside.GlobalPosition.X, _playerInside.GlobalPosition.Y + 35);
		}
		else
		{
			fish_position = new Vector2(_playerInside.GlobalPosition.X - 18, _playerInside.GlobalPosition.Y + 5);
		}
		fish.GlobalPosition = fish_position; 
	}
}
