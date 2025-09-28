using Godot;
using System;

public partial class Raccoon : Area2D
{
	private Player _playerInside = null;
	[Export] public string text;
	private Label sellFishText;
	private FishingSpot fish;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
		sellFishText = GetNode<Label>("Label");
		fish = GetNode<FishingSpot>("../../Water/FishingSpot");
		
	}
	
	private void SayLabel()
	{
			sellFishText.Text = $" {text}";
	}
	
	private void OnBodyEntered(Node body)
	{
		if (body is Player player) // or body.IsInGroup("Player")
		{
			_playerInside = player;
			text = "Press [E] to sell fish";
		}
	}

	private void OnBodyExited(Node body)
	{
		if (body is Player)
		{
			_playerInside = null;
			text = "";
			SayLabel();
		}
	}
	
	public override void _Process(double delta)
	{
		if (_playerInside != null && fish.getFish() > 0)
		{
			SayLabel();
			if (Input.IsActionJustPressed("Talk")) {
				text = "";
				SayLabel();
				fish.sellFish();
			}
		}
	}
}
