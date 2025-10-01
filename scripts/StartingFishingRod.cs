using Godot;
using System;

public partial class StartingFishingRod : Area2D
{
	private Player _playerInside = null;
	private FishingSpot readyToFish;
	private Label text;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
		readyToFish = GetNode<FishingSpot>("../../Water/FishingSpot");
		text = GetNode<Label>("Label");
		text.Text = "Press [E] to pick up";
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
		if (_playerInside != null && Input.IsActionJustPressed("Talk"))
		{
			QueueFree();
			readyToFish.pickedUpFishingRod();
		}
	}
}
