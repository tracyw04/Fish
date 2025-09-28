using Godot;
using System;

public partial class FishingSpot : Area2D
{
	private Player _playerInside = null;
	PackedScene _fishScene;
	[Export] public int numFishCaught = 0;
	private Label _countLabel;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
		_countLabel = GetNode<Label>("../../CanvasLayer/Label");
		UpdateCountLabel();
	}
	
	private void UpdateCountLabel()
	{
		_countLabel.Text = $"Fish: {numFishCaught}";
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
			numFishCaught++;
			UpdateCountLabel();
		}
	}
	
	private void SpawnFish()
	{
		var fish = (Node2D)_fishScene.Instantiate();
		GetParent().AddChild(fish);
		Vector2 fish_position;
		//right quadrant
		var facingDir = _playerInside.FacingDirection;

		if (_playerInside.GlobalPosition.X >= 60) {
			fish_position = new Vector2(GlobalPosition.X, _playerInside.GlobalPosition.Y+5);
			//top quadrant
		} else if (_playerInside.GlobalPosition.Y <= -90) {
			fish_position = new Vector2(_playerInside.GlobalPosition.X, _playerInside.GlobalPosition.Y);
			//bottom quadrant
		} else if (_playerInside.GlobalPosition.Y >= 30) {
			fish_position = new Vector2(_playerInside.GlobalPosition.X,_playerInside.GlobalPosition.Y+35);
		} else {
			fish_position = new Vector2(_playerInside.GlobalPosition.X - 18, _playerInside.GlobalPosition.Y+5);
		}
		
		GD.Print("Player position ", _playerInside.GlobalPosition);
		GD.Print("Player facing ", _playerInside.FacingDirection);
		fish.GlobalPosition = fish_position; 
		//GD.Print("Fish spawned at ", fish.GlobalPosition);
	}
}
