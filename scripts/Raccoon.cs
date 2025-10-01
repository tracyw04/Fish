using Godot;
using System;

public partial class Raccoon : Area2D
{
	private Player _playerInside = null;
	[Export] public string text;
	private Label sellFishText;
	private Store shop;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
		sellFishText = GetNode<Label>("Label");
	}
	
	private void SayLabel()
	{
			sellFishText.Text = $" {text}";
	}
	
	private void OnBodyEntered(Node body)
	{
		if (body is Player player) 
		{
			_playerInside = player;
			text = "Press [E] to sell fish\nPress[R] to open menu";
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
		if (_playerInside != null)
		{
			SayLabel();
			if (Input.IsActionJustPressed("Talk"))
			{
				text = "";
				SayLabel();
				_playerInside.makeProfit();
			} else if (Input.IsActionJustPressed("Shop")) {
				shop = GetNode<Store>("../Store");
				shop.ShowMenu();
			}
		}
	}
}
